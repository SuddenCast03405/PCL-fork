Imports System.Windows.Navigation

Public Class MyChinaConsentWindow

    Public Property Accepted As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Consent_Changed(sender As Object, e As RoutedEventArgs) Handles CheckAge.Checked, CheckAge.Unchecked, CheckEula.Checked, CheckEula.Unchecked
        BtnContinue.IsEnabled = CheckAge.IsChecked = True AndAlso CheckEula.IsChecked = True
    End Sub

    Private Sub BtnContinue_Click(sender As Object, e As RoutedEventArgs) Handles BtnContinue.Click
        If CheckAge.IsChecked <> True OrElse CheckEula.IsChecked <> True Then Return
        Accepted = True
        Close()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As RoutedEventArgs) Handles BtnExit.Click
        Close()
    End Sub

    Private Sub LinkEula_RequestNavigate(sender As Object, e As RequestNavigateEventArgs)
        e.Handled = True
        OpenWebsite(MinecraftEulaUrl)
    End Sub

End Class
