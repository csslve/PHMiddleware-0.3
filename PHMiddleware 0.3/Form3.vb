
Imports System.IO
Imports System.Data
Imports System.Xml

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadFiles()
    End Sub

    Private Sub LoadFiles()
        Dim directoryPath As String = "C:\RSLog\"
        If Directory.Exists(directoryPath) Then
            Dim files = Directory.GetFiles(directoryPath, "*.xml")
            ListBoxFiles.Items.Clear()
            For Each file In files
                ListBoxFiles.Items.Add(Path.GetFileName(file))
            Next
        Else
            MessageBox.Show("La cartella non esiste.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ListBoxFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxFiles.SelectedIndexChanged
        If ListBoxFiles.SelectedItem IsNot Nothing Then
            Dim selectedFileName = ListBoxFiles.SelectedItem.ToString()
            Dim filePath = Path.Combine("C:\RSLog\", selectedFileName)
            LoadXmlData(filePath)

            ' Aggiorna TextBox1 con il nome del file senza estensione
            TextBox1.Text = Path.GetFileNameWithoutExtension(selectedFileName)
            TextBox2.Text = File.GetCreationTime(filePath).ToString("dd/MM/yyyy HH:mm:ss")
        End If
    End Sub

    Private Sub LoadXmlData(filePath As String)
        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(filePath)

            ' Creare un DataTable per contenere i dati XML
            Dim dataTable As New DataTable()

            ' Aggiungi colonne al DataTable
            dataTable.Columns.Add("RSNUM")
            dataTable.Columns.Add("RSPOS")
            dataTable.Columns.Add("ZDATACR")
            dataTable.Columns.Add("BDTER")
            dataTable.Columns.Add("WERKS")
            dataTable.Columns.Add("LGORT")
            dataTable.Columns.Add("LGOBE_LGORT")
            dataTable.Columns.Add("KOSTL")
            dataTable.Columns.Add("UMLGO")
            dataTable.Columns.Add("LGOBE_UMLGO")
            dataTable.Columns.Add("ZDOC")
            dataTable.Columns.Add("DESCR")
            dataTable.Columns.Add("MATNR")
            dataTable.Columns.Add("MAKTX")
            dataTable.Columns.Add("ERFMG")
            dataTable.Columns.Add("ERFME")
            dataTable.Columns.Add("ENMNG")

            ' Caricare i dati dal documento XML
            Dim items = xmlDoc.SelectNodes("//LT_OUTPUT/item")
            For Each item As XmlNode In items
                Dim row As DataRow = dataTable.NewRow()
                row("RSNUM") = item.SelectSingleNode("RSNUM")?.InnerText
                row("RSPOS") = item.SelectSingleNode("RSPOS")?.InnerText
                row("ZDATACR") = item.SelectSingleNode("ZDATACR")?.InnerText
                row("BDTER") = item.SelectSingleNode("BDTER")?.InnerText
                row("WERKS") = item.SelectSingleNode("WERKS")?.InnerText
                row("LGORT") = item.SelectSingleNode("LGORT")?.InnerText
                row("LGOBE_LGORT") = item.SelectSingleNode("LGOBE_LGORT")?.InnerText
                row("KOSTL") = item.SelectSingleNode("KOSTL")?.InnerText
                row("UMLGO") = item.SelectSingleNode("UMLGO")?.InnerText
                row("LGOBE_UMLGO") = item.SelectSingleNode("LGOBE_UMLGO")?.InnerText
                row("ZDOC") = item.SelectSingleNode("ZDOC")?.InnerText
                row("DESCR") = item.SelectSingleNode("DESCR")?.InnerText
                row("MATNR") = item.SelectSingleNode("MATNR")?.InnerText
                row("MAKTX") = item.SelectSingleNode("MAKTX")?.InnerText
                row("ERFMG") = item.SelectSingleNode("ERFMG")?.InnerText
                row("ERFME") = item.SelectSingleNode("ERFME")?.InnerText
                row("ENMNG") = item.SelectSingleNode("ENMNG")?.InnerText
                dataTable.Rows.Add(row)
            Next

            ' Imposta il DataGridView come sorgente dati
            DataGridView1.DataSource = dataTable

        Catch ex As Exception
            MessageBox.Show("Errore nel caricamento del file XML: " & ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
