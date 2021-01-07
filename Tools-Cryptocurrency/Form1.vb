Imports System.Net
Imports Microsoft.VisualBasic.CompilerServices
Imports System.IO
Public Class Form1
    ' ############################################################
    ' ######################## API SETTING ' #####################
    Dim usb_thb As String = "https://hi.in.th/api/usd-thb.php" ' 1 ดอลล่า = บาทไทยปัจจุบัน
    Dim coinmarketcap_bitcoin As String = "https://hi.in.th/api/bitcoin-coinmarketcap.php" ' coinmarketcap
    Dim bitkub As String = "https://hi.in.th/api/bitcoin-bitkub.php" ' bitkub
    Dim pingping As String = "https://hi.in.th/api/ping.php" 'ping test
    Dim server As String = "https://hi.in.th/api/server.ini" ' server version
    Dim version As String = "1.0.0.1" ' version

    ' ######################## DATA API ##########################
    ' My.Settings.thb = 1 ดอล แปลงเป็น บาท
    ' My.Settings.coinmarketcap_usb = ราคา BTC ปัจจุบัน ตลาด coinmarketcap usb
    ' My.Settings.coinmarketcap_thb = ราคา BTC ปัจจุบัน ตลาด coinmarketcap thb
    ' My.Settings.bx_usb = ราคา BTC ปัจจุบัน ตลาด bx usb
    ' My.Settings.bx_thb = ราคา BTC ปัจจุบัน ตลาด bx thb
    ' ############################################################


    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'version
        Label12.Text = Version
        'เริ่มต้นระบบออโต้รันโค๊ด ทุกๆ 30 วินาที / 30000 s
        reload.Start()
        'ตัวอัพเดทข้อมูลให้เป็นปัจจุบัน
        update.Start()
        ' รีเซ็ตเงื่อนไขทุกครั้งที่ทำการเปิดโปรแกรมนี้ใหม่
        My.Settings.checked = "0"
        My.Settings.loop1 = "0"
        My.Settings.loop2 = "0"
        My.Settings.loop3 = "0"
        My.Settings.loop4 = "0"
        My.Settings.thb = "0"
        My.Settings.dot = "0"
        ' loading
        loadingpng.Start()



        'สั่งเริ่มดึง API ครั้งแรกก่อนเข้าลูป
        'อัพเดทค่าเงินปัจจุบัน
        web_usb_to_thb.Navigate(usb_thb)
        'coinmarketcap_bitcoin
        ex1.Navigate(coinmarketcap_bitcoin)
        'bx_bitcoin
        ex2.Navigate(bitkub)
        'ping test
        ping.Navigate(pingping)
        'version server checked
        serververion.Navigate(server)


        'ปิดไม่ให้คำนวณจนกว่าจะโหลด API เสร็จ
        ComboBox1.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

        ' table exchange at radio
        RadioButton1.Checked = True
        ' alert system radio
        RadioButton3.Checked = True
        ' alert server
        RadioButton5.Checked = True
    End Sub

    Private Sub reload_Tick(sender As Object, e As EventArgs) Handles reload.Tick
        'อัพเดทค่าเงินปัจจุบัน
        web_usb_to_thb.Navigate(usb_thb)
        'coinmarketcap_bitcoin
        ex1.Navigate(coinmarketcap_bitcoin)
        'bx_bitcoin
        ex2.Navigate(bitkub)
        'ping test
        ping.Navigate(pingping)
        'version server checked
        serververion.Navigate(server)

        'loading pic
        PictureBox3.Show()
        loadingpng.Start()
    End Sub

    Private Sub update_Tick(sender As Object, e As EventArgs) Handles update.Tick
        ' แสดงผลเมื่อดึงข้อมูลจาก API สำเร็จ 
            Try
            thb.Text = FormatNumber(web_usb_to_thb.DocumentText, 2)
            My.Settings.thb = FormatNumber(web_usb_to_thb.DocumentText, 2)
            'ถ้าตัวแปรนี้มีค่ามากกว่า 4 จึงจะสามารถใช้ระบบคำนวณได้
            If My.Settings.loop1 < 1 Then

                If My.Settings.checked < 4 Then
                    My.Settings.checked += 1
                    My.Settings.loop1 += 1
                End If
            End If
            ' end if loop
            Catch ex As Exception

            End Try
            


        ' ############# Start API 1 ############
        ' แสดงผลเมื่อดึงข้อมูลจาก API สำเร็จ 
        Try
            coinmarketcap_usb.Text = FormatNumber(ex1.DocumentText, 2) + " $"
            ' แปลงเป็นค่าเงินบาทไทย
            coinmarketcap_thb.Text = FormatNumber(coinmarketcap_usb.Text * thb.Text, 2) + " บาท"
            ' บันทึกเข้าฐานข้อมูล ใช้สำหรับการคำนวณในโปรแกรม
            My.Settings.coinmarketcap_usb = FormatNumber(ex1.DocumentText, 2)
            My.Settings.coinmarketcap_thb = FormatNumber(coinmarketcap_usb.Text * thb.Text, 2)

            'ถ้าตัวแปรนี้มีค่ามากกว่า 4 จึงจะสามารถใช้ระบบคำนวณได้
            If My.Settings.loop2 < 1 Then

                If My.Settings.checked < 4 Then
                    My.Settings.checked += 1
                    My.Settings.loop2 += 1
                End If
            End If
        Catch ex As Exception

        End Try
        

        ' ############# End API 1 #############


        ' ############# Start API 2 ################
        Try

            bx_usb.Text = FormatNumber(ex2.DocumentText, 2) + " $"
            ' แปลงเป็นค่าเงินบาทไทย
            bx_thb.Text = FormatNumber(bx_usb.Text * thb.Text, 2) + " บาท"
            ' บันทึกเข้าฐานข้อมูล ใช้สำหรับการคำนวณในโปรแกรม
            My.Settings.bx_usb = FormatNumber(ex2.DocumentText, 2)
            My.Settings.bx_thb = FormatNumber(bx_usb.Text * thb.Text, 2)

            'ถ้าตัวแปรนี้มีค่ามากกว่า 4 จึงจะสามารถใช้ระบบคำนวณได้
            If My.Settings.loop3 < 1 Then

                If My.Settings.checked < 4 Then
                    My.Settings.checked += 1
                    My.Settings.loop3 += 1
                End If
            End If
            ' end if loop


        Catch ex As Exception

        End Try
        
        ' ############# End API 2 ##################

        'เช็คปิง

        Try
            'โชว์ค่าปิงเซิร์ฟเวอร์
            pingshow.Text = ping.DocumentText
            pingstats.ForeColor = Color.Red
            'ถ้าตัวแปรนี้มีค่ามากกว่า 4 จึงจะสามารถใช้ระบบคำนวณได้
            If My.Settings.loop4 < 1 Then

                If My.Settings.checked < 4 Then
                    My.Settings.checked += 1
                    My.Settings.loop4 += 1
                End If

            End If
            If pingshow.Text <= 100 Then
                pingstats.Value = pingshow.Text
            End If
            ' end if loop
        Catch ex As Exception

        End Try



        'แสดงผลเวลาปัจจุบัน อ้างอิงจากเครื่อง
        ToolStripStatusLabel2.Text = Now.ToLongTimeString.ToString()
        ToolStripStatusLabel3.Text = DateTime.Now.ToLongDateString()


        'version checked
        Try
            Labelversionserver.Text = serververion.DocumentText
        Catch ex As Exception

        End Try
        






        ' progressbar ready api
        If My.Settings.checked <= 4 Then
            apistats.Value = My.Settings.checked
        End If


        ' เปิดใช้งานให้คำนวณ
        If My.Settings.checked = 4 Then
            ComboBox1.Enabled = True
            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True
        End If


        'select system combobox
        If ComboBox1.Text = "[coinmarketcap] BTC แปลงเป็น THB" Then
            pic1.Image = My.Resources.BTC
            pic2.Image = My.Resources.equalsz
            pic3.Image = My.Resources.thb
            pic4.Image = My.Resources.BTC
            pic5.Image = My.Resources.thb
        ElseIf ComboBox1.Text = "[coinmarketcap] BTC แปลงเป็น USD" Then
            pic1.Image = My.Resources.BTC
            pic2.Image = My.Resources.equalsz
            pic3.Image = My.Resources.USD
            pic4.Image = My.Resources.BTC
            pic5.Image = My.Resources.USD
        ElseIf ComboBox1.Text = "[bitkub.com] BTC แปลงเป็น THB" Then
            pic1.Image = My.Resources.BTC
            pic2.Image = My.Resources.equalsz
            pic3.Image = My.Resources.thb
            pic4.Image = My.Resources.BTC
            pic5.Image = My.Resources.thb
        ElseIf ComboBox1.Text = "[bitkub.com] BTC แปลงเป็น USD" Then
            pic1.Image = My.Resources.BTC
            pic2.Image = My.Resources.equalsz
            pic3.Image = My.Resources.USD
            pic4.Image = My.Resources.BTC
            pic5.Image = My.Resources.USD

        ElseIf ComboBox1.Text = "$ เป็น ฿" Then
            pic1.Image = My.Resources.USD
            pic2.Image = My.Resources.equalsz
            pic3.Image = My.Resources.thb
            pic4.Image = My.Resources.USD
            pic5.Image = My.Resources.thb
        ElseIf ComboBox1.Text = "฿ เป็น $" Then
            pic1.Image = My.Resources.thb
            pic2.Image = My.Resources.equalsz
            pic3.Image = My.Resources.USD
            pic4.Image = My.Resources.thb
            pic5.Image = My.Resources.USD
        End If





        ' ######################## DATA API ##########################
        ' My.Settings.thb = 1 ดอล แปลงเป็น บาท
        ' My.Settings.coinmarketcap_usb = ราคา BTC ปัจจุบัน ตลาด coinmarketcap usb
        ' My.Settings.coinmarketcap_thb = ราคา BTC ปัจจุบัน ตลาด coinmarketcap thb
        ' My.Settings.bx_usb = ราคา BTC ปัจจุบัน ตลาด bx usb
        ' My.Settings.bx_thb = ราคา BTC ปัจจุบัน ตลาด bx thb
        ' ############################################################
        ' คำนวณ


        If ComboBox1.Text = "[coinmarketcap] BTC แปลงเป็น THB" Then
            ' เมื่อไม่ใส่ค่าก็จะทำให้ไม่สามารถคำนวณได้
            If TextBox1.Text = "" Then
            ElseIf TextBox1.Text = " " Then
            ElseIf TextBox3.Text = "" Then
            ElseIf TextBox3.Text = " " Then
            Else
                TextBox2.Text = FormatNumber((My.Settings.coinmarketcap_thb * TextBox1.Text) * TextBox3.Text, 2)
            End If

        ElseIf ComboBox1.Text = "[coinmarketcap] BTC แปลงเป็น USD" Then
            ' เมื่อไม่ใส่ค่าก็จะทำให้ไม่สามารถคำนวณได้
            If TextBox1.Text = "" Then
            ElseIf TextBox1.Text = " " Then
            ElseIf TextBox3.Text = "" Then
            ElseIf TextBox3.Text = " " Then
            Else
                TextBox2.Text = FormatNumber((My.Settings.coinmarketcap_usb * TextBox1.Text) * TextBox3.Text, 2)
            End If
        ElseIf ComboBox1.Text = "[bitkub.com] BTC แปลงเป็น THB" Then
            ' เมื่อไม่ใส่ค่าก็จะทำให้ไม่สามารถคำนวณได้
            If TextBox1.Text = "" Then
            ElseIf TextBox1.Text = " " Then
            ElseIf TextBox3.Text = "" Then
            ElseIf TextBox3.Text = " " Then
            Else
                TextBox2.Text = FormatNumber((My.Settings.bx_thb * TextBox1.Text) * TextBox3.Text, 2)
            End If
        ElseIf ComboBox1.Text = "[bitkub.com] BTC แปลงเป็น USD" Then
            ' เมื่อไม่ใส่ค่าก็จะทำให้ไม่สามารถคำนวณได้
            If TextBox1.Text = "" Then
            ElseIf TextBox1.Text = " " Then
            ElseIf TextBox3.Text = "" Then
            ElseIf TextBox3.Text = " " Then
            Else
                TextBox2.Text = FormatNumber((My.Settings.bx_usb * TextBox1.Text) * TextBox3.Text, 2)
            End If

        ElseIf ComboBox1.Text = "$ เป็น ฿" Then
            ' เมื่อไม่ใส่ค่าก็จะทำให้ไม่สามารถคำนวณได้
            If TextBox1.Text = "" Then
            ElseIf TextBox1.Text = " " Then
            ElseIf TextBox3.Text = "" Then
            ElseIf TextBox3.Text = " " Then
            Else
                TextBox2.Text = FormatNumber((TextBox1.Text * My.Settings.thb) * TextBox3.Text, 2)
            End If
        ElseIf ComboBox1.Text = "฿ เป็น $" Then
            ' เมื่อไม่ใส่ค่าก็จะทำให้ไม่สามารถคำนวณได้
            If TextBox1.Text = "" Then
            ElseIf TextBox1.Text = " " Then
            ElseIf TextBox3.Text = "" Then
            ElseIf TextBox3.Text = " " Then
            Else
                TextBox2.Text = FormatNumber((TextBox1.Text / My.Settings.thb) * TextBox3.Text, 2)
            End If


        End If





        ' table stats
        If RadioButton1.Checked = True Then
            'bx
            nn2.Text = FormatNumber((nn1.Text * My.Settings.bx_thb), 2)
            nn4.Text = FormatNumber((nn3.Text * My.Settings.bx_thb), 2)
            nn6.Text = FormatNumber((nn5.Text * My.Settings.bx_thb), 2)
            nn8.Text = FormatNumber((nn7.Text * My.Settings.bx_thb), 2)
            nn10.Text = FormatNumber((nn9.Text * My.Settings.bx_thb), 2)
            nn12.Text = FormatNumber((nn11.Text * My.Settings.bx_thb), 2)
            nn14.Text = FormatNumber((nn13.Text * My.Settings.bx_thb), 2)
            nn16.Text = FormatNumber((nn15.Text * My.Settings.bx_thb), 2)
            nn18.Text = FormatNumber((nn17.Text * My.Settings.bx_thb), 2)
            nn20.Text = FormatNumber((nn19.Text * My.Settings.bx_thb), 2)
            nn22.Text = FormatNumber((nn21.Text * My.Settings.bx_thb), 2)
            nn24.Text = FormatNumber((nn23.Text * My.Settings.bx_thb), 2)
            nn26.Text = FormatNumber((nn25.Text * My.Settings.bx_thb), 2)
            nn28.Text = FormatNumber((nn27.Text * My.Settings.bx_thb), 2)

            'coinmarketcap
            mm2.Text = FormatNumber((mm1.Text * My.Settings.coinmarketcap_thb), 2)
            mm4.Text = FormatNumber((mm3.Text * My.Settings.coinmarketcap_thb), 2)
            mm6.Text = FormatNumber((mm5.Text * My.Settings.coinmarketcap_thb), 2)
            mm8.Text = FormatNumber((mm7.Text * My.Settings.coinmarketcap_thb), 2)
            mm10.Text = FormatNumber((mm9.Text * My.Settings.coinmarketcap_thb), 2)
            mm12.Text = FormatNumber((mm11.Text * My.Settings.coinmarketcap_thb), 2)
            mm14.Text = FormatNumber((mm13.Text * My.Settings.coinmarketcap_thb), 2)
            mm16.Text = FormatNumber((mm15.Text * My.Settings.coinmarketcap_thb), 2)
            mm18.Text = FormatNumber((mm17.Text * My.Settings.coinmarketcap_thb), 2)
            mm20.Text = FormatNumber((mm19.Text * My.Settings.coinmarketcap_thb), 2)
            mm22.Text = FormatNumber((mm21.Text * My.Settings.coinmarketcap_thb), 2)
            mm24.Text = FormatNumber((mm23.Text * My.Settings.coinmarketcap_thb), 2)
            mm26.Text = FormatNumber((mm25.Text * My.Settings.coinmarketcap_thb), 2)
            mm28.Text = FormatNumber((mm27.Text * My.Settings.coinmarketcap_thb), 2)
        End If

        ' table stats
        If RadioButton2.Checked = True Then
            'bx
            nn2.Text = FormatNumber((nn1.Text * My.Settings.bx_usb), 2)
            nn4.Text = FormatNumber((nn3.Text * My.Settings.bx_usb), 2)
            nn6.Text = FormatNumber((nn5.Text * My.Settings.bx_usb), 2)
            nn8.Text = FormatNumber((nn7.Text * My.Settings.bx_usb), 2)
            nn10.Text = FormatNumber((nn9.Text * My.Settings.bx_usb), 2)
            nn12.Text = FormatNumber((nn11.Text * My.Settings.bx_usb), 2)
            nn14.Text = FormatNumber((nn13.Text * My.Settings.bx_usb), 2)
            nn16.Text = FormatNumber((nn15.Text * My.Settings.bx_usb), 2)
            nn18.Text = FormatNumber((nn17.Text * My.Settings.bx_usb), 2)
            nn20.Text = FormatNumber((nn19.Text * My.Settings.bx_usb), 2)
            nn22.Text = FormatNumber((nn21.Text * My.Settings.bx_usb), 2)
            nn24.Text = FormatNumber((nn23.Text * My.Settings.bx_usb), 2)
            nn26.Text = FormatNumber((nn25.Text * My.Settings.bx_usb), 2)
            nn28.Text = FormatNumber((nn27.Text * My.Settings.bx_usb), 2)

            'coinmarketcap
            mm2.Text = FormatNumber((mm1.Text * My.Settings.coinmarketcap_usb), 2)
            mm4.Text = FormatNumber((mm3.Text * My.Settings.coinmarketcap_usb), 2)
            mm6.Text = FormatNumber((mm5.Text * My.Settings.coinmarketcap_usb), 2)
            mm8.Text = FormatNumber((mm7.Text * My.Settings.coinmarketcap_usb), 2)
            mm10.Text = FormatNumber((mm9.Text * My.Settings.coinmarketcap_usb), 2)
            mm12.Text = FormatNumber((mm11.Text * My.Settings.coinmarketcap_usb), 2)
            mm14.Text = FormatNumber((mm13.Text * My.Settings.coinmarketcap_usb), 2)
            mm16.Text = FormatNumber((mm15.Text * My.Settings.coinmarketcap_usb), 2)
            mm18.Text = FormatNumber((mm17.Text * My.Settings.coinmarketcap_usb), 2)
            mm20.Text = FormatNumber((mm19.Text * My.Settings.coinmarketcap_usb), 2)
            mm22.Text = FormatNumber((mm21.Text * My.Settings.coinmarketcap_usb), 2)
            mm24.Text = FormatNumber((mm23.Text * My.Settings.coinmarketcap_usb), 2)
            mm26.Text = FormatNumber((mm25.Text * My.Settings.coinmarketcap_usb), 2)
            mm28.Text = FormatNumber((mm27.Text * My.Settings.coinmarketcap_usb), 2)
        End If



        If TextBox4.Text = " " Then
            Button1.Enabled = False
        ElseIf TextBox4.Text = "" Then
            Button1.Enabled = False
        Else
            Button1.Enabled = True
        End If
    End Sub

    Private Sub loadingpng_Tick(sender As Object, e As EventArgs) Handles loadingpng.Tick
        PictureBox3.Hide()
        loadingpng.Stop()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)





    End Sub

    
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = "1"
    End Sub

 

    Private Sub CToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CToolStripMenuItem.Click
        MsgBox("1024", , "Note")
        Process.Start("https://www.facebook.com/1024HQ/")
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) And Not e.KeyChar = "." Then
            e.Handled = True
        End If

    End Sub
   
    
    Private Sub alert_Tick(sender As Object, e As EventArgs) Handles alert.Tick
        If TextBox4.Text = " " Then
        ElseIf TextBox4.Text = "" Then

        Else
            ' server bx.in.th
            If RadioButton5.Checked = True Then
                ' มากกว่า
                If RadioButton3.Checked = True Then
                    If My.Settings.bx_thb >= TextBox4.Text Then
                        My.Computer.Audio.Play(My.Resources.btcsound, AudioPlayMode.Background)
                    End If
                End If
                ' น้อยกว่า
                If RadioButton4.Checked = True Then
                    If My.Settings.bx_thb <= TextBox4.Text Then
                        My.Computer.Audio.Play(My.Resources.btcsound, AudioPlayMode.Background)
                    End If
                End If
            End If

            ' server coinmarketcap
            If RadioButton6.Checked = True Then
                ' มากกว่า
                If RadioButton3.Checked = True Then
                    If My.Settings.coinmarketcap_thb >= TextBox4.Text Then
                        My.Computer.Audio.Play(My.Resources.btcsound, AudioPlayMode.Background)
                    End If
                End If
                ' น้อยกว่า
                If RadioButton4.Checked = True Then
                    If My.Settings.coinmarketcap_thb <= TextBox4.Text Then
                        My.Computer.Audio.Play(My.Resources.btcsound, AudioPlayMode.Background)
                    End If
                End If

            End If

        End If


    End Sub

    

    Private Sub TextBox4__KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) And Not e.KeyChar = "." Then
            e.Handled = True
        End If
        'Me.TextBox4.Text = FormatNumber(Me.TextBox4.Text, 2, , , TriState.True)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "START" Then
            Button1.Text = "STOP"
            TextBox4.ReadOnly = True
            Me.TextBox4.Text = FormatNumber(Me.TextBox4.Text, 2, , , TriState.True)
            alert.Start()
        ElseIf Button1.Text = "STOP" Then
            Button1.Text = "START"
            TextBox4.ReadOnly = False
            alert.Stop()
        End If

       
    End Sub

    Private Sub GithubToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GithubToolStripMenuItem.Click
        Process.Start("https://github.com/1024HQ/Tool-Cryptocurrency")
    End Sub
End Class
