Imports System.Globalization

''' <summary>
''' 中国大陆地区的启动确认。
''' </summary>
Public Module ModChinaConsent

    Public Const MinecraftEulaUrl As String = "https://www.minecraft.net/eula"

    Private MainlandChinaCache As Boolean? = Nothing

    ''' <summary>
    ''' 是否为中国大陆地区。优先使用 IP 地理位置，失败时回退到 Windows 区域设置。
    ''' </summary>
    Public Function IsMainlandChina() As Boolean
        If MainlandChinaCache.HasValue Then Return MainlandChinaCache.Value
        Dim Result As Boolean? = DetectMainlandByIp()
        Dim IsByIp As Boolean = Result.HasValue
        If Not Result.HasValue Then Result = DetectMainlandBySystemRegion()
        MainlandChinaCache = Result.Value
        Logger.Info($"中国大陆地区识别：{If(MainlandChinaCache.Value, "是", "否")}（{If(IsByIp, "IP 地理位置", "系统区域设置")}）")
        Return MainlandChinaCache.Value
    End Function

    Private Function DetectMainlandByIp() As Boolean?
        'ipip.net 返回形如“当前 IP：1.2.3.4  来自于：中国 北京  联通”
        Try
            Dim Text As String = NetRequestByClient("https://myip.ipip.net", Timeout:=1500, MakeLog:=False).Trim()
            Dim Mark As Integer = Text.IndexOf("来自于：", StringComparison.Ordinal)
            If Mark >= 0 Then
                Dim Location As String = Text.Substring(Mark + 4).Trim()
                If Location.StartsWithF("中国", True) Then
                    Return Not (Location.Contains("香港") OrElse Location.Contains("澳门") OrElse Location.Contains("台湾"))
                End If
                If Location.Length > 0 Then Return False
            End If
        Catch ex As Exception
            Logger.Warn(ex, "通过 ipip.net 识别地区失败")
        End Try
        'ipapi.co 返回两位国家代码
        Try
            Dim Country As String = NetRequestByClient("https://ipapi.co/country/", Timeout:=1500, MakeLog:=False).Trim().ToUpperInvariant()
            If Country.Length = 2 Then Return Country = "CN"
        Catch ex As Exception
            Logger.Warn(ex, "通过 ipapi.co 识别地区失败")
        End Try
        Return Nothing
    End Function

    Private Function DetectMainlandBySystemRegion() As Boolean
        Try
            Return RegionInfo.CurrentRegion.TwoLetterISORegionName.Equals("CN", StringComparison.OrdinalIgnoreCase)
        Catch ex As Exception
            Logger.Warn(ex, "读取系统区域设置失败")
            Return False
        End Try
    End Function

End Module
