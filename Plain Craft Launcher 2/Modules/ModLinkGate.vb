''' <summary>
''' 内置联机功能的启用开关与使用前声明。
''' </summary>
Public Module ModLinkGate

    ''' <summary>
    ''' 仅当可执行文件目录的 PCL\Setup.ini 中存在 EasyTier=true 时启用联机功能。
    ''' </summary>
    Public Function IsEasyTierEnabled() As Boolean
        Try
            Dim SetupFile As String = Paths.Base & "PCL\Setup.ini"
            If Not FileUtils.Exists(SetupFile) Then Return False
            For Each RawLine In FileUtils.ReadAsString(SetupFile).Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                Dim Entry As String = RawLine.Trim().TrimStart(ChrW(&HFEFF))
                Dim SplitIndex As Integer = Entry.IndexOf("="c)
                If SplitIndex < 0 Then SplitIndex = Entry.IndexOf(":"c)
                If SplitIndex <= 0 Then Continue For
                If Entry.Substring(0, SplitIndex).Trim().Equals("EasyTier", StringComparison.OrdinalIgnoreCase) Then
                    Return Entry.Substring(SplitIndex + 1).Trim().Equals("true", StringComparison.OrdinalIgnoreCase)
                End If
            Next
            Return False
        Catch ex As Exception
            Logger.Warn(ex, "读取内置联机开关失败")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 每次进入联机功能时显示特别声明与免责条款，全部勾选后才返回 True。
    ''' </summary>
    Public Function ConfirmDisclaimer() As Boolean
        Dim Accepted As Boolean = False
        RunInUiWait(
        Sub()
            Dim Dialog As New MyLinkDisclaimerWindow
            Dialog.ShowDialog()
            Accepted = Dialog.Accepted
        End Sub)
        Return Accepted
    End Function

End Module
