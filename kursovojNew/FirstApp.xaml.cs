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
//using System.Windows.Shapes;
using System.IO;

namespace kursovojNew
{
    /// <summary>
    /// Логика взаимодействия для FirstApp.xaml
    /// </summary>
    /// 

    public static class randList
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count-1;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public static class myMethods
    {

        public static void begin(Button play, Button sound1, Button sound2)
        {
            play.Content = "Играть музыку";
            sound1.Visibility = Visibility.Visible;
            sound2.Visibility = Visibility.Visible;
        }

        public static void showRightImg(Uri uri, List<string> sounds, List<string> images, int numSound, Image img)
        {
            for (int i = 0; i < images.Count; i++)
            {
               if (Path.GetFileNameWithoutExtension(sounds[numSound]) == Path.GetFileNameWithoutExtension(images[i]))
                {
                    uri = new Uri(images[i]);
                    img.Source = new BitmapImage(uri);
                }
            }
        }

        public static void endApp(List<string> sounds, int trueAnswersCount, int numerOfSound)
        {
                FirstAppAnswers window = new FirstAppAnswers();
                window.lblAnswers.Content = "Правильных ответов " + trueAnswersCount + " из " + sounds.Count;
                window.Show();
                numerOfSound = 0;
        }

        public static void answerCounter(List<string> sound, string[] lines, int numerOfSound,ref int trueAnswers)
        {
            for (int i = 0; i < lines.Length; i++)
                if (Path.GetFileNameWithoutExtension(sound[numerOfSound]) == lines[i])
                    trueAnswers++;
        }
    }

    public partial class FirstApp : Window
    {
        System.Media.SoundPlayer player = new System.Media.SoundPlayer();
        List<string> randSounds = new List<string>();
        List<string> randImages = new List<string>();

        string[] soundSLines = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "soundsS.txt");
        string[] soundShLines = System.IO.File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "soundsSh.txt");

        

        Uri uri = null;
        int trueAnswers = 0;
        int numSound = 0;

        public FirstApp()
        {
            InitializeComponent();

            randImages.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory+"images"));
            randSounds.AddRange(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory+"sounds"));

            randList.Shuffle<string>(randSounds);
            
            player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "dimdimych.wav";
            uri = new Uri(AppDomain.CurrentDomain.BaseDirectory+ "dimdimych.png");
            imgTask.Source = new BitmapImage(uri);
        }

        private void clickBtnPlay(object sender, RoutedEventArgs e)
        {
            myMethods.begin(btnPlay, btnSoundS, btnSoundSh);
            myMethods.showRightImg(uri, randSounds, randImages, numSound, imgTask);
            player.Play();
            player.SoundLocation = randSounds[numSound];
        }

        private void clickBtnSoundSh(object sender, RoutedEventArgs e)
        {
            numSound++;
            if(numSound > randSounds.Count-1)
            {
                myMethods.endApp(randSounds, trueAnswers, numSound);
                this.Close();
                numSound = 0;
            }
            myMethods.showRightImg(uri, randSounds, randImages, numSound, imgTask);
            myMethods.answerCounter(randSounds, soundShLines, numSound,ref trueAnswers);
            player.SoundLocation = randSounds[numSound];
        }

        private void clickBtnSoundS(object sender, RoutedEventArgs e)
        {
            numSound++;
            if (numSound > randSounds.Count - 1)
            {
                myMethods.endApp(randSounds, trueAnswers, numSound);
                this.Close();
                numSound = 0;
            }
            myMethods.showRightImg(uri, randSounds, randImages, numSound, imgTask);
            myMethods.answerCounter(randSounds, soundSLines, numSound,ref trueAnswers);
            player.SoundLocation = randSounds[numSound];
        }
    }
}
