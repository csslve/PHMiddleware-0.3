Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Xml.XPath

Public Class Form1
    Public Class YourResultType
        ' Definizione delle proprietà
        Public Property RSNUM As String
        Public Property RSPOS As String
        Public Property ZDATACR As String
        Public Property BDTER As String
        Public Property WERKS As String
        Public Property LGORT As String
        Public Property LGOBE_LGORT As String
        Public Property KOSTL As String
        Public Property UMLGO As String
        Public Property LGOBE_UMLGO As String
        Public Property ZDOC As String
        Public Property DESCR As String
        Public Property MATNR As String
        Public Property MAKTX As String
        Public Property ERFMG As String
        Public Property ERFME As String
        Public Property ENMNG As String
    End Class

    Private results As New List(Of YourResultType)()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dataGridResults.AutoGenerateColumns = True
        btnSearch.Enabled = False ' Disabilita il pulsante all'avvio
        Esporta.Enabled = False ' Disabilita il pulsante Esporta all'avvio
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Abilita il pulsante solo se c'è del testo in txtSearch
        btnSearch.Enabled = Not String.IsNullOrWhiteSpace(txtSearch.Text)
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Questa è la guida dell'applicazione.", "Guida", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim username As String = "ws_user"
        Dim password As String = "soresa"
        Dim lgort As String = txtLGORT.Text
        Dim matnr As String = txtMATNR.Text
        Dim rsnum As String = txtSearch.Text
        Dim werks As String = txtWERKS.Text

        Try
            results = SearchWebService(username, password, lgort, matnr, rsnum, werks)
            If results.Count = 0 Then
                MessageBox.Show("Nessun dato trovato.")
                Esporta.Enabled = False ' Disabilita Esporta se non ci sono dati
            Else
                LoadDataIntoGrid()
                SaveXmlResponseAutomatically(results.FirstOrDefault())
            End If
        Catch ex As Exception
            MessageBox.Show("Errore: " & ex.Message)
        End Try
    End Sub

    Private Function SearchWebService(username As String, password As String, lgort As String, matnr As String, rsnum As String, werks As String) As List(Of YourResultType)
        Dim url As String = "http://93.42.19.183:8000/sap/bc/srt/rfc/sap/zmm_richieste_buster/800/zmm_richieste_buster/zmm_richieste_buster"
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/soap+xml; charset=utf-8"

        Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"))
        request.Headers("Authorization") = "Basic " & credentials

        Dim soapEnvelope As String = $"
        <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:urn=""urn:sap-com:document:sap:rfc:functions"">
            <soap:Header/>
            <soap:Body>
                <urn:ZMM_RICHIESTE_BUSTER>
                    <LGORT>{lgort}</LGORT>
                    <MATNR>{matnr}</MATNR>
                    <RSNUM>{rsnum}</RSNUM>
                    <WERKS>{werks}</WERKS>
                </urn:ZMM_RICHIESTE_BUSTER>
            </soap:Body>
        </soap:Envelope>"

        Using streamWriter As New StreamWriter(request.GetRequestStream())
            streamWriter.Write(soapEnvelope)
        End Using

        Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        Dim responseString As String

        Using reader As New StreamReader(response.GetResponseStream())
            responseString = reader.ReadToEnd()
        End Using

        txtResponse.Text = responseString

        Return ProcessResponse(responseString)
    End Function

    Private Sub SaveXmlResponseAutomatically(result As YourResultType)
        Try
            If result IsNot Nothing AndAlso Not String.IsNullOrEmpty(result.RSNUM) Then
                ' Specifica il percorso di salvataggio
                Dim directoryPath As String = "C:\RSLog\" ' Cambia questo percorso come necessario
                If Not Directory.Exists(directoryPath) Then
                    Directory.CreateDirectory(directoryPath)
                End If

                ' Imposta il nome base del file
                Dim baseFileName As String = Path.Combine(directoryPath, $"{result.RSNUM}.xml")
                Dim fileName As String = baseFileName

                ' Controlla se il file esiste già
                If File.Exists(fileName) Then
                    Dim resultDialog As DialogResult = MessageBox.Show("Il numero di richiesta è gia presente in archivio. Procedere comunque?", "Avviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)

                    If resultDialog = DialogResult.Cancel Then
                        Esporta.Enabled = False
                        ' Esci dalla funzione se l'operatore annulla
                        Return
                    End If

                    ' Se l'operatore ha scelto OK, aggiungi un numero progressivo al nome del file
                    Dim fileIndex As Integer = 1
                    Do While File.Exists(fileName)
                        fileName = Path.Combine(directoryPath, $"{result.RSNUM}_{fileIndex}.xml")
                        fileIndex += 1
                    Loop
                End If

                Using writer As New StreamWriter(fileName, False, Encoding.UTF8)
                    writer.Write(txtResponse.Text) ' Utilizza il contenuto della risposta come XML
                End Using
                MessageBox.Show($"Richiesta archiviata con successo '{Path.GetFileName(fileName)}'!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Numero richiesta non disponibile. Impossibile continuare.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show($"Si è verificato un errore durante il salvataggio del file XML: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Function ProcessResponse(responseString As String) As List(Of YourResultType)
        Dim results As New List(Of YourResultType)()
        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.LoadXml(responseString)

            Dim navigator As XPathNavigator = xmlDoc.CreateNavigator()
            Dim namespaceManager As New XmlNamespaceManager(xmlDoc.NameTable)
            namespaceManager.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope")
            namespaceManager.AddNamespace("urn", "urn:sap-com:document:sap:rfc:functions")

            Dim itemPath As String = "//LT_OUTPUT/item"
            Dim items As XPathNodeIterator = navigator.Select(itemPath, namespaceManager)

            If Not items.MoveNext() Then
                MessageBox.Show("Nessun item trovato. Controlla la risposta XML.")
                Return results
            End If

            Do
                Dim result As New YourResultType()
                Dim itemNavigator As XPathNavigator = items.Current

                result.RSNUM = itemNavigator.SelectSingleNode("RSNUM")?.Value
                result.RSPOS = itemNavigator.SelectSingleNode("RSPOS")?.Value
                result.ZDATACR = itemNavigator.SelectSingleNode("ZDATACR")?.Value
                result.BDTER = itemNavigator.SelectSingleNode("BDTER")?.Value
                result.WERKS = itemNavigator.SelectSingleNode("WERKS")?.Value
                result.LGORT = itemNavigator.SelectSingleNode("LGORT")?.Value
                result.LGOBE_LGORT = itemNavigator.SelectSingleNode("LGOBE_LGORT")?.Value
                result.KOSTL = itemNavigator.SelectSingleNode("KOSTL")?.Value
                result.UMLGO = itemNavigator.SelectSingleNode("UMLGO")?.Value
                result.LGOBE_UMLGO = itemNavigator.SelectSingleNode("LGOBE_UMLGO")?.Value
                result.ZDOC = itemNavigator.SelectSingleNode("ZDOC")?.Value
                result.DESCR = itemNavigator.SelectSingleNode("DESCR")?.Value
                result.MATNR = itemNavigator.SelectSingleNode("MATNR")?.Value
                result.MAKTX = itemNavigator.SelectSingleNode("MAKTX")?.Value
                result.ERFMG = itemNavigator.SelectSingleNode("ERFMG")?.Value
                result.ERFME = itemNavigator.SelectSingleNode("ERFME")?.Value
                result.ENMNG = itemNavigator.SelectSingleNode("ENMNG")?.Value

                results.Add(result)
            Loop While items.MoveNext()
        Catch ex As Exception
            MessageBox.Show("Errore durante il parsing della risposta XML: " & ex.Message)
        End Try
        Return results
    End Function

    Private Sub LoadDataIntoGrid()
        Dim table As New DataTable()
        table.Columns.Add("RSNUM")
        table.Columns.Add("RSPOS")
        table.Columns.Add("ZDATACR")
        table.Columns.Add("BDTER")
        table.Columns.Add("WERKS")
        table.Columns.Add("LGORT")
        table.Columns.Add("LGOBE_LGORT")
        table.Columns.Add("KOSTL")
        table.Columns.Add("UMLGO")
        table.Columns.Add("LGOBE_UMLGO")
        table.Columns.Add("ZDOC")
        table.Columns.Add("DESCR")
        table.Columns.Add("MATNR")
        table.Columns.Add("MAKTX")
        table.Columns.Add("ERFMG")
        table.Columns.Add("ERFME")
        table.Columns.Add("ENMNG")

        For Each result In results
            table.Rows.Add(
                If(result.RSNUM, ""),
                If(result.RSPOS, ""),
                If(result.ZDATACR, ""),
                If(result.BDTER, ""),
                If(result.WERKS, ""),
                If(result.LGORT, ""),
                If(result.LGOBE_LGORT, ""),
                If(result.KOSTL, ""),
                If(result.UMLGO, ""),
                If(result.LGOBE_UMLGO, ""),
                If(result.ZDOC, ""),
                If(result.DESCR, ""),
                If(result.MATNR, ""),
                If(result.MAKTX, ""),
                If(result.ERFMG, ""),
                If(result.ERFME, ""),
                If(result.ENMNG, "")
            )
        Next

        dataGridResults.DataSource = table

        ' Controlla se il DataGrid è vuoto e abilita/disabilita il pulsante "Esporta"
        Esporta.Enabled = table.Rows.Count > 0
    End Sub

    Private Sub Esporta_Click(sender As Object, e As EventArgs) Handles Esporta.Click
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
        saveFileDialog.Title = "Esporta dati"
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            Using writer As New StreamWriter(saveFileDialog.FileName, False, Encoding.UTF8)
                For Each result In results
                    Dim line = String.Join(",",
                        If(result.RSNUM, ""),
                        If(result.RSPOS, ""),
                        If(result.ZDATACR, ""),
                        If(result.BDTER, ""),
                        If(result.WERKS, ""),
                        If(result.LGORT, ""),
                        If(result.LGOBE_LGORT, ""),
                        If(result.KOSTL, ""),
                        If(result.UMLGO, ""),
                        If(result.LGOBE_UMLGO, ""),
                        If(result.ZDOC, ""),
                        If(result.DESCR, ""),
                        If(result.MATNR, ""),
                        If(result.MAKTX, ""),
                        If(result.ERFMG, ""),
                        If(result.ERFME, ""),
                        If(result.ENMNG, "")
                    )
                    writer.WriteLine(line)
                Next
            End Using

            MessageBox.Show("Richiesta creata con successo!", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSearch.PerformClick() ' Simula il click del pulsante di ricerca
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.Close()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Controlla se la combinazione di tasti Ctrl + Shift + F è premuta
        If e.Control AndAlso e.Shift AndAlso e.KeyCode = Keys.F Then
            ' Apri Form3
            Dim form3 As New Form3()
            form3.Show()
        End If
    End Sub
End Class
