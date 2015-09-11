using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;

//using System.Windows.Automation;


namespace Voice_Recognition
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synth = new SpeechSynthesizer();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new String[] { "say hello", "what is my name", "you are ugly", "zezocas", "shutdown computer", "lockdown computer", "open task manager", "open command line", "open chrome", "test" });
            GrammarBuilder gBuilder = new GrammarBuilder(); // Put into speech recognizition engine the commands you gonna use
            gBuilder.Append(commands);
            Grammar grammer = new Grammar(gBuilder); // pass through the objects from grammar builder

            recEngine.LoadGrammarAsync(grammer); // loads grammar
            recEngine.SetInputToDefaultAudioDevice(); // set input ( mic)
            recEngine.SpeechRecognized +=recEngine_SpeechRecognized;

            synth.SelectVoiceByHints(VoiceGender.Female);
            
            

        }

        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch(e.Result.Text) // gets the string from speech
            {
                case "say hello":
                    //MessageBox.Show("Hello Cmdr Exceltior.How are you?");
                    synth.SpeakAsync("Hello Commander. I'm Arta. How are you?");
                    break;
                case "what is my name":
                    synth.SpeakAsync("your name is King Exceltior");
                    richTextBox1.Text += "\nJoao";
                    break;

                case "you are ugly":
                    synth.SpeakAsync("you are ugly too!");
                    richTextBox1.Text += "\n you are ugly recognized!";
                    break;
                case "zezocas":
                    synth.SpeakAsync("Activated");
                    richTextBox1.Text += "\nzezocas recognized!";
                    SendKeys.Send("{Break}");
                    break;
                case "shutdown computer":
                    synth.SpeakAsync("Shutdown Initiated...");
                    Process.Start("shutdown", "/s /t 0");
                    break;
                case "lockdown computer":
                    synth.SpeakAsync("Lockdown Initiated...");
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
                    break;
                case "open task manager":
                    synth.SpeakAsync("Accessing System Core...");
                    foreach (Process procs in Process.GetProcessesByName("taskmgr"))
                    {
                        procs.Kill();
                    }
                    synth.SpeakAsync("Access Granted...");
                    synth.SpeakAsync("Loading Core... Loading Data...");
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\taskmgr.exe");
                    /*
                    Process p = new Process();
                    p.StartInfo.FileName = " taskmgr";
                    p.StartInfo.CreateNoWindow = true;
                    p.Start(); // comeca processo
                    Console.WriteLine("Waiting for handle...");
                    */
                    richTextBox1.Text += "\nOpening taskmanager";
                    
                    //while (p.MainWindowHandle == IntPtr.Zero) ;

                    //AutomationElement aDesktop = AutomationElement.RootElement;
                    //AutomationElement aForm = AutomationElement.FromHandle(p.MainWindowHandle);
                    //Console.WriteLine("Got handle");

                    //Get Tabs controls

                    //AutomationElement aTabs = aForm.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Tab));

                    // Get Collection of tab pages
                    //AutomationElementCollection aTabItems = aTabs.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem));


                    //set focus to the performace tab

                    //AutomationElement aPerformanceTab = aTabItems[3];
                    //aPerformaceTab.SetFocus();

                    break;

                case "open command line":
                    synth.SpeakAsync("Accessing System Core...");
                    System.Diagnostics.Process.Start(@"C:\WINDOWS\system32\cmd.exe");
                    break;

                case "open chrome":
                    synth.SpeakAsync("Opening Browser...");
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\Chrome.exe");
                    break;
                case "test":
                    synth.SpeakAsync("The System Cores are Healthy...");
                    break;
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDisable.Enabled = true;
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDisable.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
