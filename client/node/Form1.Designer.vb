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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.txt_jobs = New System.Windows.Forms.RichTextBox()
        Me.txt_status = New System.Windows.Forms.TextBox()
        Me.btn_login = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.t1_jobs_list = New System.Windows.Forms.Timer(Me.components)
        Me.lb_current_job = New System.Windows.Forms.Label()
        Me.lb_job = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lb_nodes = New System.Windows.Forms.Label()
        Me.t2_count_nodes = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'txt_jobs
        '
        Me.txt_jobs.Location = New System.Drawing.Point(12, 166)
        Me.txt_jobs.Name = "txt_jobs"
        Me.txt_jobs.Size = New System.Drawing.Size(612, 244)
        Me.txt_jobs.TabIndex = 1
        Me.txt_jobs.Text = ""
        '
        'txt_status
        '
        Me.txt_status.BackColor = System.Drawing.Color.DodgerBlue
        Me.txt_status.Font = New System.Drawing.Font("Segoe UI Semibold", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.txt_status.Location = New System.Drawing.Point(-4, 563)
        Me.txt_status.Margin = New System.Windows.Forms.Padding(0)
        Me.txt_status.Name = "txt_status"
        Me.txt_status.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txt_status.Size = New System.Drawing.Size(644, 30)
        Me.txt_status.TabIndex = 2
        Me.txt_status.Text = "   Connecting to the main server..."
        '
        'btn_login
        '
        Me.btn_login.BackgroundImage = CType(resources.GetObject("btn_login.BackgroundImage"), System.Drawing.Image)
        Me.btn_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btn_login.Enabled = False
        Me.btn_login.Location = New System.Drawing.Point(514, 12)
        Me.btn_login.Name = "btn_login"
        Me.btn_login.Size = New System.Drawing.Size(50, 50)
        Me.btn_login.TabIndex = 3
        Me.btn_login.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Button1.Location = New System.Drawing.Point(574, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(50, 50)
        Me.Button1.TabIndex = 4
        Me.Button1.UseVisualStyleBackColor = True
        '
        't1_jobs_list
        '
        Me.t1_jobs_list.Enabled = True
        Me.t1_jobs_list.Interval = 5000
        '
        'lb_current_job
        '
        Me.lb_current_job.AutoSize = True
        Me.lb_current_job.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.lb_current_job.Location = New System.Drawing.Point(72, 83)
        Me.lb_current_job.Name = "lb_current_job"
        Me.lb_current_job.Size = New System.Drawing.Size(53, 23)
        Me.lb_current_job.TabIndex = 5
        Me.lb_current_job.Text = "0 of 0"
        '
        'lb_job
        '
        Me.lb_job.AutoSize = True
        Me.lb_job.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lb_job.Location = New System.Drawing.Point(19, 83)
        Me.lb_job.Name = "lb_job"
        Me.lb_job.Size = New System.Drawing.Size(47, 23)
        Me.lb_job.TabIndex = 6
        Me.lb_job.Text = "JOB:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 19.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(12, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 45)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Network"
        '
        'lb_nodes
        '
        Me.lb_nodes.BackColor = System.Drawing.Color.Navy
        Me.lb_nodes.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lb_nodes.ForeColor = System.Drawing.Color.White
        Me.lb_nodes.Location = New System.Drawing.Point(528, 568)
        Me.lb_nodes.Name = "lb_nodes"
        Me.lb_nodes.Size = New System.Drawing.Size(104, 20)
        Me.lb_nodes.TabIndex = 8
        Me.lb_nodes.Text = "current nodes"
        Me.lb_nodes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lb_nodes.Visible = False
        '
        't2_count_nodes
        '
        Me.t2_count_nodes.Enabled = True
        Me.t2_count_nodes.Interval = 15000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(636, 591)
        Me.Controls.Add(Me.lb_nodes)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lb_job)
        Me.Controls.Add(Me.lb_current_job)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btn_login)
        Me.Controls.Add(Me.txt_status)
        Me.Controls.Add(Me.txt_jobs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txt_jobs As RichTextBox
    Friend WithEvents txt_status As TextBox
    Friend WithEvents btn_login As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents t1_jobs_list As Timer
    Friend WithEvents lb_current_job As Label
    Friend WithEvents lb_job As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lb_nodes As Label
    Friend WithEvents t2_count_nodes As Timer
End Class
