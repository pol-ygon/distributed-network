Imports System.Net
Imports Newtonsoft.Json
Imports System.Net.NetworkInformation
Imports System.Management
Public Class Form1
    Public Property root_dir As String = "C:\\Users\\pamar\\Desktop\\work\\"

    Private Function status_message(color As Color, message As String) As String
        txt_status.BackColor = color
        txt_status.Text = "  " & message
    End Function

    Function getMacAddress()
        Dim nics() As NetworkInterface =
              NetworkInterface.GetAllNetworkInterfaces
        Return nics(0).GetPhysicalAddress.ToString
    End Function

    Function getSerialBios()
        Dim q As New SelectQuery("Win32_bios")
        Dim search As New ManagementObjectSearcher(q)

        Return (search.Get(0)("serialnumber")).ToString
    End Function

    Function getToken()
        Dim fileReader As System.IO.StreamReader
        Dim code As String

        Try
            fileReader = My.Computer.FileSystem.OpenTextFileReader(root_dir & "token.loki")
            code = fileReader.ReadLine()
            fileReader.Close()
        Catch ex As Exception
            code = ""
        End Try

        Return code
    End Function

    Function setToken(sn As String)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(root_dir & "token.loki", False)
        file.WriteLine(sn)
        file.Close()
    End Function

    Private Sub Timer1_Timer()
        request_jobs()
    End Sub

    Private Function request_jobs() As String
        Dim ID As New List(Of String)()
        Dim DESC As New List(Of String)()
        Dim SERIAL As New List(Of String)()
        Dim TYPE As New List(Of String)()

        Dim json As String
        Dim jobs As List(Of REQUEST_NEW_JOB)

        '//// Before applying new job, nodes must check if they are registered in!
        '//// Integrity
        Dim token As String = getToken()
        Dim bios As String = getSerialBios()
        Dim mac As String = getMacAddress()

        If token.Equals("") Then
            status_message(Color.Orange, "⚠️ Node not registered")
            lb_nodes.Visible = False
            btn_login.Enabled = True
        Else

            Try
                ' Initializing web request
                ServicePointManager.Expect100Continue = True
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                json = (New WebClient).DownloadString("http://127.0.0.1:5000/jobs?token=" & token & "&bios=" & bios & "&mac=" & mac)

                ' Deserialize the JSON string.
                jobs = JsonConvert.DeserializeObject(Of List(Of REQUEST_NEW_JOB))(json)

                ' checking jobs
                If jobs.Count > 0 And jobs(0).status = 200 Then                                     ' job found + status node valid!
                    status_message(Color.Lime, "🔨 Jobs in queue (" & jobs.Count & ")")
                    lb_current_job.Text = "0 of " & jobs.Count - 1
                    btn_login.Enabled = False

                    lb_nodes.Visible = True
                    lb_nodes.BackColor = Color.LimeGreen

                    For i As Integer = 0 To jobs.Count - 1
                        ID.Add(jobs(i).local_id)
                        DESC.Add(jobs(i).desc)
                        SERIAL.Add(jobs(i).serial)
                        TYPE.Add(jobs(i).type)
                    Next

                    ''''' TO DO '''''

                ElseIf jobs.Count = 0 And jobs(0).status = 200 Then                                 ' node valid but no job to do
                    status_message(Color.MediumPurple, "✔️ There are no jobs in queue!")
                    lb_current_job.Text = "0 of 0"
                    btn_login.Enabled = False
                ElseIf jobs(0).status = 400 Then                                                    ' node is corrupted
                    status_message(Color.Yellow, "☠️ Your node is corrupted")
                    lb_current_job.Text = "0 of 0"

                    lb_nodes.Visible = False
                    btn_login.Enabled = False
                End If

            Catch ex As WebException
                status_message(Color.Red, "🔌 Can't connect to the server!")
                lb_current_job.Text = "UNKNOWN"
                lb_nodes.Visible = False
                btn_login.Enabled = False
            End Try

        End If

    End Function


    Private Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click

        Dim json As String
        Dim tk As List(Of REQUEST_NEW_JOB)

        Dim mac As String = getMacAddress()
        Dim bios As String = getSerialBios()
        Dim serial As String = getToken()

        If serial.Equals("") Then
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            json = (New WebClient).DownloadString("http://127.0.0.1:5000/register_node?bios=" & bios & "&mac=" & mac)

            tk = JsonConvert.DeserializeObject(Of List(Of REQUEST_NEW_JOB))(json)

            setToken((tk(0).token).Trim)
        Else
            MessageBox.Show("Node already registered!")
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles t1_jobs_list.Tick
        request_jobs()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim proc As Process = New Process
        proc.StartInfo.FileName = "C:\Users\pamar\AppData\Local\Programs\Python\Python38\python.exe" 'Default Python Installation
        proc.StartInfo.Arguments = "C:\Users\pamar\AppData\Local\Programs\Python\Python38\sun.py"
        proc.StartInfo.UseShellExecute = False 'required for redirect.
        proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden 'don't show commandprompt.
        proc.StartInfo.CreateNoWindow = True
        proc.StartInfo.RedirectStandardOutput = True 'captures output from commandprompt.
        proc.Start()
        AddHandler proc.OutputDataReceived, AddressOf proccess_OutputDataReceived
        proc.BeginOutputReadLine()
        proc.WaitForExit()

        txt_jobs.Text += Value
        lb_nodes.Text = "-- nodes"

    End Sub

    Public Sub proccess_OutputDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        On Error Resume Next
        If e.Data = "" Then
        Else
            Value = e.Data
        End If
    End Sub

    Private Sub t2_count_nodes_Tick(sender As Object, e As EventArgs) Handles t2_count_nodes.Tick

        Dim json As String
        Dim nodes As List(Of REQUEST_NEW_JOB)

        '//// Before applying new job, nodes must check if they are registered in!
        '//// Integrity
        Dim token As String = getToken()
        Dim bios As String = getSerialBios()
        Dim mac As String = getMacAddress()

        If Not token.Equals("") Then

            Try
                ' Initializing web request
                ServicePointManager.Expect100Continue = True
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                json = (New WebClient).DownloadString("http://127.0.0.1:5000/online_nodes?token=" & token & "&bios=" & bios & "&mac=" & mac)

                ' Deserialize the JSON string.
                nodes = JsonConvert.DeserializeObject(Of List(Of REQUEST_NEW_JOB))(json)

                lb_nodes.Text = nodes(0).nodes & " nodes"

            Catch ex As WebException

            End Try

        End If
    End Sub
End Class
