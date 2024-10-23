<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        btnSearch = New Button()
        dataGridResults = New DataGridView()
        YourResultTypeBindingSource = New BindingSource(components)
        txtSearch = New TextBox()
        Esporta = New Button()
        txtWERKS = New TextBox()
        txtMATNR = New TextBox()
        txtLGORT = New TextBox()
        txtResponse = New RichTextBox()
        Button1 = New Button()
        PictureBox1 = New PictureBox()
        Label1 = New Label()
        CType(dataGridResults, ComponentModel.ISupportInitialize).BeginInit()
        CType(YourResultTypeBindingSource, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnSearch
        ' 
        btnSearch.Location = New Point(561, 45)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(328, 24)
        btnSearch.TabIndex = 2
        btnSearch.Text = "Cerca"
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' dataGridResults
        ' 
        dataGridResults.BackgroundColor = Color.FromArgb(CByte(192), CByte(255), CByte(192))
        dataGridResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dataGridResults.Location = New Point(27, 75)
        dataGridResults.Name = "dataGridResults"
        dataGridResults.ReadOnly = True
        dataGridResults.Size = New Size(862, 365)
        dataGridResults.TabIndex = 1
        ' 
        ' YourResultTypeBindingSource
        ' 
        YourResultTypeBindingSource.DataSource = GetType(_3.YourResultType)
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(668, 16)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(221, 23)
        txtSearch.TabIndex = 1
        ' 
        ' Esporta
        ' 
        Esporta.Font = New Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Esporta.Location = New Point(27, 548)
        Esporta.Name = "Esporta"
        Esporta.Size = New Size(252, 40)
        Esporta.TabIndex = 3
        Esporta.Text = "Esporta"
        Esporta.UseVisualStyleBackColor = True
        ' 
        ' txtWERKS
        ' 
        txtWERKS.Location = New Point(27, 674)
        txtWERKS.Name = "txtWERKS"
        txtWERKS.Size = New Size(100, 23)
        txtWERKS.TabIndex = 4
        txtWERKS.Text = "HO01"
        ' 
        ' txtMATNR
        ' 
        txtMATNR.Location = New Point(409, 674)
        txtMATNR.Name = "txtMATNR"
        txtMATNR.Size = New Size(100, 23)
        txtMATNR.TabIndex = 5
        ' 
        ' txtLGORT
        ' 
        txtLGORT.Location = New Point(765, 674)
        txtLGORT.Name = "txtLGORT"
        txtLGORT.Size = New Size(100, 23)
        txtLGORT.TabIndex = 6
        txtLGORT.Text = "H01"
        ' 
        ' txtResponse
        ' 
        txtResponse.Location = New Point(27, 446)
        txtResponse.Name = "txtResponse"
        txtResponse.Size = New Size(862, 96)
        txtResponse.TabIndex = 7
        txtResponse.Text = ""
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.Location = New Point(637, 548)
        Button1.Name = "Button1"
        Button1.Size = New Size(252, 40)
        Button1.TabIndex = 4
        Button1.Text = "Esci"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(27, 12)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(170, 57)
        PictureBox1.TabIndex = 9
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(561, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(101, 15)
        Label1.TabIndex = 10
        Label1.Text = "Numero Richiesta"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(915, 601)
        ControlBox = False
        Controls.Add(Label1)
        Controls.Add(PictureBox1)
        Controls.Add(Button1)
        Controls.Add(txtResponse)
        Controls.Add(txtLGORT)
        Controls.Add(txtMATNR)
        Controls.Add(txtWERKS)
        Controls.Add(Esporta)
        Controls.Add(txtSearch)
        Controls.Add(dataGridResults)
        Controls.Add(btnSearch)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        KeyPreview = True
        Name = "Form1"
        Text = "PHMiddleware 0.5"
        CType(dataGridResults, ComponentModel.ISupportInitialize).EndInit()
        CType(YourResultTypeBindingSource, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnSearch As Button
    Friend WithEvents dataGridResults As DataGridView
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents Esporta As Button
    Friend WithEvents txtWERKS As TextBox
    Friend WithEvents txtMATNR As TextBox
    Friend WithEvents txtLGORT As TextBox
    Friend WithEvents txtResponse As RichTextBox
    Friend WithEvents YourResultTypeBindingSource As BindingSource
    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label

End Class
