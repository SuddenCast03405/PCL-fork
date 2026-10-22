Imports System.Windows.Navigation

Public Class MyChinaConsentWindow

    Public Property Accepted As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub CheckConsent_Change(sender As Object, user As Boolean) Handles CheckConsent.Change
        BtnContinue.IsEnabled = CheckConsent.Checked
    End Sub

    Private Sub BtnContinue_Click(sender As Object, e As MouseButtonEventArgs) Handles BtnContinue.Click
        If Not CheckConsent.Checked Then Return
        Accepted = True
        Close()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As MouseButtonEventArgs) Handles BtnExit.Click
        Close()
    End Sub

    Private Sub LinkEula_RequestNavigate(sender As Object, e As RequestNavigateEventArgs)
        e.Handled = True
        OpenWebsite(MinecraftEulaUrl)
    End Sub

    Private Sub Window_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles Me.MouseLeftButtonDown
        On Error Resume Next
        DragMove()
    End Sub

End Class
