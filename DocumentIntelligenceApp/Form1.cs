using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure.Core;
using Azure.Core.Pipeline;
using System.Net.Http;

namespace DocumentIntelligenceApp;

public partial class Form1 : Form
{
    // Azure Document Intelligence configuration
    private const string EndpointUrl = "APIM UDL";
    private const string ApiKey = "APIM Subscription Key";
    private const string JwtToken = "JWT Token";

    // Store the client globally
    private DocumentAnalysisClient _client;

    public Form1()
    {
        InitializeComponent();
        
        // Initialize the Document Intelligence client with API key and JWT token
        var options = new DocumentAnalysisClientOptions
        {
            Transport = new HttpClientTransport(new HttpClient())
        };
        options.AddPolicy(new AddHeadersPolicy(
            ("api-key", ApiKey),
            ("Authorization", $"Bearer {JwtToken}")
        ), HttpPipelinePosition.PerCall);
        
        _client = new DocumentAnalysisClient(new Uri(EndpointUrl), new AzureKeyCredential("dummy"), options);
    }

    // Custom policy to add multiple headers
    private class AddHeadersPolicy : HttpPipelineSynchronousPolicy
    {
        private readonly (string name, string value)[] _headers;

        public AddHeadersPolicy(params (string name, string value)[] headers)
        {
            _headers = headers;
        }

        public override void OnSendingRequest(HttpMessage message)
        {
            foreach (var (name, value) in _headers)
            {
                message.Request.Headers.Add(name, value);
            }
        }
    }
    
    private async void btnUpload_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            openFileDialog.Title = "Select a PDF File";
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtOutput.Text = "Processing document...";
                    btnUpload.Enabled = false;
                    
                    string filePath = openFileDialog.FileName;
                    await ProcessDocument(filePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOutput.Text = $"Error processing document: {ex.Message}";
                }
                finally
                {
                    btnUpload.Enabled = true;
                }
            }
        }
    }
    
    private async Task ProcessDocument(string filePath)
    {
        try
        {
            using (FileStream stream = File.OpenRead(filePath))
            {
                // Start the document analysis
                AnalyzeDocumentOperation operation = await _client.AnalyzeDocumentAsync(WaitUntil.Completed, "prebuilt-document", stream);

                // Get the analysis results
                AnalyzeResult result = operation.Value;

                // Display the results
                txtOutput.Clear();
                txtOutput.AppendText($"Document contains {result.Pages.Count} page(s)\r\n\r\n");

                // Add the content
                txtOutput.AppendText("Document Content:\r\n");
                txtOutput.AppendText("----------------\r\n");
                foreach (DocumentPage page in result.Pages)
                {
                    foreach (DocumentLine line in page.Lines)
                    {
                        txtOutput.AppendText($"{line.Content}\r\n");
                    }
                    txtOutput.AppendText("\r\n");
                }

                // Add key-value pairs if available
                if (result.KeyValuePairs.Count > 0)
                {
                    txtOutput.AppendText("\r\nExtracted Key-Value Pairs:\r\n");
                    txtOutput.AppendText("-------------------------\r\n");

                    foreach (DocumentKeyValuePair kvp in result.KeyValuePairs)
                    {
                        string key = kvp.Key?.Content ?? "N/A";
                        string value = kvp.Value?.Content ?? "N/A";
                        txtOutput.AppendText($"{key}: {value}\r\n");
                    }
                }

                // Add tables if available
                if (result.Tables.Count > 0)
                {
                    txtOutput.AppendText("\r\nExtracted Tables:\r\n");
                    txtOutput.AppendText("----------------\r\n");

                    int tableIndex = 1;
                    foreach (DocumentTable table in result.Tables)
                    {
                        txtOutput.AppendText($"Table {tableIndex++} - {table.RowCount} rows x {table.ColumnCount} columns\r\n");

                        // Process cells for simple display
                        for (int r = 0; r < table.RowCount; r++)
                        {
                            for (int c = 0; c < table.ColumnCount; c++)
                            {
                                // Find cells for current position
                                DocumentTableCell? cell = table.Cells.FirstOrDefault(
                                    tc => tc.RowIndex == r && tc.ColumnIndex == c);
                                string content = cell?.Content ?? "";
                                txtOutput.AppendText($"[{content}] ");
                            }
                            txtOutput.AppendText("\r\n");
                        }
                        txtOutput.AppendText("\r\n");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            txtOutput.Text = $"Error analyzing document: {ex.Message}";
            throw; // Rethrow for outer handler
        }
    }
}
