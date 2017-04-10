using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Speech.Recognition;

namespace kursovojNew
{
    /// <summary>
    /// Логика взаимодействия для SecondApp.xaml
    /// </summary>

    public class MyMethods
    {
        public static void showImage(Uri uri, List<string> images, Image img, Button btn, int massStep)
        {
            btn.Content = "Далее";
            uri = new Uri(images[massStep]);
            img.Source = new BitmapImage(uri);
            btn.Visibility = Visibility.Hidden;
            massStep++;
        }
    }

    public class Speech
    {
        public Uri Uri { get; set; }
        public Button Btn { get; set; }
        public int MassStep { get; set; }
        public List<string> Images { get; set; }
        public string[] Lines { get; set; }
        public Image Img { get; set; }

        public void work(SpeechRecognizedEventArgs e)
        {
            MassStep++;
            Btn.Visibility = Visibility.Visible;
            if (MassStep > Images.Count - 1)
            {
                MassStep = 0;
                FirstAppAnswers window = new FirstAppAnswers();
                window.lblAnswers.Content = "Ты помог Лунтику!!";
                window.Show();
            }
            
        }

        public void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show(e.Result.Text);
            MessageBox.Show(MassStep.ToString());
            for (int i = 0; i < Lines.Length; i++)
                if (e.Result.Confidence > 0.75 && MassStep == i && e.Result.Text == Lines[i])
                    work(e);
        }

        public void SpRec()
        {

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);

            Choices words = new Choices();
            words.Add(Lines);

            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(words);

            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }
    }

    public partial class SecondApp : Window
    {
        static List<string> images = new List<string>();
        string[] words = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "SecondAppWords.txt");
        Uri uri = null;
        int massStep = 0;
        Speech myEngine = new Speech();

        

        public SecondApp()
        {
            InitializeComponent();
            images.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "secondAppImages"));

            uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "secondAppBackground.jpg");
            imgTaskSecondApp.Source = new BitmapImage(uri);

            myEngine.Btn = btnNext;
            myEngine.Images = images;
            myEngine.Lines = words;
            myEngine.MassStep = massStep;
            myEngine.Uri = uri;
            myEngine.Img = imgTaskSecondApp;
        }

        private void clickBtnNext(object sender, RoutedEventArgs e)
        {
            MyMethods.showImage(uri, images, imgTaskSecondApp, btnNext, massStep);
            massStep++;
            myEngine.SpRec();
            
        }

        private void clickBtnReboot(object sender, RoutedEventArgs e)
        {
            
            myEngine.SpRec();
        }
    }
}
