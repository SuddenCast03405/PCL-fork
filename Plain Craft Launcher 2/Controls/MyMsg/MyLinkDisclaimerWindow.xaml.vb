Public Class MyLinkDisclaimerWindow

    Public Property Accepted As Boolean = False

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Disclaimer_Changed(sender As Object, e As RoutedEventArgs) Handles CheckAvailability.Checked, CheckAvailability.Unchecked, CheckPrivacy.Checked, CheckPrivacy.Unchecked, CheckLoss.Checked, CheckLoss.Unchecked, CheckChangeRight.Checked, CheckChangeRight.Unchecked, CheckLaw.Checked, CheckLaw.Unchecked
        BtnAccept.IsEnabled = CheckAvailability.IsChecked = True AndAlso CheckPrivacy.IsChecked = True AndAlso
                              CheckLoss.IsChecked = True AndAlso CheckChangeRight.IsChecked = True AndAlso CheckLaw.IsChecked = True
    End Sub

    Private Sub BtnAccept_Click(sender As Object, e As RoutedEventArgs) Handles BtnAccept.Click
        If CheckAvailability.IsChecked <> True OrElse CheckPrivacy.IsChecked <> True OrElse CheckLoss.IsChecked <> True OrElse
           CheckChangeRight.IsChecked <> True OrElse CheckLaw.IsChecked <> True Then Return
        Accepted = True
        Close()
    End Sub

    Private Sub BtnDecline_Click(sender As Object, e As RoutedEventArgs) Handles BtnDecline.Click
        Close()
    End Sub

End Class
