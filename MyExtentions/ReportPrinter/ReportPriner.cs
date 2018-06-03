using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Drawing;

public class ReportPriner : IDisposable
{
    private int m_currentPageIndex;
    private IList<Stream> m_streams;
    private ReportDataSourceCollection DataSources;
    private object p;
    private const string PAGE_SETTINGS= @"<DeviceInfo>
                <OutputFormat>{0}</OutputFormat>
                <PageWidth>{1}</PageWidth>
                <PageHeight>{2}</PageHeight>
                <MarginTop>{3}</MarginTop>
                <MarginLeft>{4}</MarginLeft>
                <MarginRight>{5}</MarginRight>
                <MarginBottom>{6}</MarginBottom>
            </DeviceInfo>";
    private const string DEFAULT_OUTPUT_FORMAT = @"EMF";

    private String ReportPath { get; set; }
    private ReportDataSource DataSource { get; set; }
    private ReportParameter[] ReportParameter { get; set; }
    public ReportPriner(String ReportPath, ReportDataSource DataSource, ReportParameter[] ReportParameter)
    {
        this.ReportPath = ReportPath;
        this.DataSource = DataSource;
        this.ReportParameter = ReportParameter;
    }

    public ReportPriner(string reportPath, ReportDataSourceCollection dataSources, object p)
    {
        ReportPath = reportPath;
        this.DataSources = dataSources;
        this.p = p;
    }

    private DataTable LoadSalesData()
    {
        // Create a new DataSet and read sales data file 
        //    data.xml into the first DataTable.
        DataSet dataSet = new DataSet();
        dataSet.ReadXml(@"..\..\data.xml");
        return dataSet.Tables[0];
    }
    // Routine to provide to the report renderer, in order to
    //    save an image for each page of the report.
    private Stream CreateStream(string name,
      string fileNameExtension, Encoding encoding,
      string mimeType, bool willSeek)
    {
        Stream stream = new MemoryStream();
        m_streams.Add(stream);
        return stream;
    }
    // Export the given report as an EMF (Enhanced Metafile) file.
    private void Export(LocalReport report, PageSettings settings = null)
    {
        string deviceInfo =
            settings == null ?
          @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0in</MarginTop>
                <MarginLeft>0in</MarginLeft>
                <MarginRight>0in</MarginRight>
                <MarginBottom>0in</MarginBottom>
            </DeviceInfo>" :
            string.Format(PAGE_SETTINGS, new string[] { DEFAULT_OUTPUT_FORMAT,
            (settings.PaperSize.Width/100).ToString()+"in",
            (settings.PaperSize.Height/100).ToString()+"in",
            (settings.Margins.Top/100).ToString()+"in",
            (settings.Margins.Left/100).ToString()+"in",
            (settings.Margins.Right/100).ToString()+"in",
            (settings.Margins.Bottom/100).ToString()+"in"
            });
        Warning[] warnings;
        m_streams = new List<Stream>();
        report.Render("Image", deviceInfo, CreateStream,
           out warnings);
        foreach (Stream stream in m_streams)
            stream.Position = 0;
    }
    // Handler for PrintPageEvents
    private void PrintPage(object sender, PrintPageEventArgs ev)
    {
        Metafile pageImage = new
           Metafile(m_streams[m_currentPageIndex]);

        // Adjust rectangular area with printer margins.
        Rectangle adjustedRect = new Rectangle(
            ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
            ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
            ev.PageBounds.Width,
            ev.PageBounds.Height);

        // Draw a white background for the report
        ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

        // Draw the report content
        ev.Graphics.DrawImage(pageImage, adjustedRect);

        // Prepare for the next page. Make sure we haven't hit the end.
        m_currentPageIndex++;
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
    }

    private void Print(String PrinterName)
    {
        if (m_streams == null || m_streams.Count == 0)
            throw new Exception("Error: no stream to print.");
        PrintDocument printDoc = new PrintDocument();
        printDoc.PrinterSettings.PrinterName = PrinterName;
        if (!printDoc.PrinterSettings.IsValid)
        {
            throw new Exception("Error: cannot find the default printer.");
        }
        else
        {
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            m_currentPageIndex = 0;
            printDoc.Print();
        }
    }
    // Create a local report for Report.rdlc, load the data,
    //    export the report to an .emf file, and print it.
    public void Run(String PrinterName)
    {
        LocalReport report = new LocalReport();
        //report.ReportPath = @"..\..\Report.rdlc";@"LSExtendedWarrenty.WarrentySlip.rdlc"     
        report.ReportEmbeddedResource = this.ReportPath;
        if (DataSources == null)
            report.DataSources.Add(DataSource);
        else
            foreach (var x in DataSources)
                report.DataSources.Add(x);
        report.SetParameters(ReportParameter == null ? new ReportParameter[0] : ReportParameter);
        Export(report);
        try
        {
            Print(PrinterName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Dispose()
    {
        if (m_streams != null)
        {
            foreach (Stream stream in m_streams)
                stream.Close();
            m_streams = null;
        }
    }

    public LocalReport LocalReport
    {
        get
        {

            LocalReport report = new LocalReport();
            //report.ReportPath = @"..\..\Report.rdlc";@"LSExtendedWarrenty.WarrentySlip.rdlc"     
            report.ReportEmbeddedResource = this.ReportPath;
            report.DataSources.Add(DataSource);
            report.SetParameters(ReportParameter);
            return report;
        }
    }
}