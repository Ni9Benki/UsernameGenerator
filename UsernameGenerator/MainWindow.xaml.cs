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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using System.Reflection;
using System.Windows.Threading;

namespace UsernameGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Generate generator = new Generate();
        Save saver = new Save();
        Read reader = new Read();

        SoundPlayer player = new SoundPlayer();

        private List<string> languages = new List<string> { "English", "Swedish" };

        public MainWindow()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };

            InitializeComponent();

            generator.SetWordsInLists();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;

            foreach (var language in languages)
            {
                languageComboBox.Items.Add(language);
            }
            
            languageComboBox.SelectedItem = languages[0];

            player.Stream = Properties.Resources.music;
            player.PlayLooping();
        }

        private void generateUsernameButton_Click(object sender, RoutedEventArgs e)
        {
            usernameTextBox.Text = generator.GenerateUsername(languageComboBox.Text);
        }

        private void saveUsernameButton_Click(object sender, RoutedEventArgs e)
        {
            MakeFadeOut();

            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                SystemSounds.Asterisk.Play();
                messageTextBox.Text = "Username field is blank!";
                return;
            }

            if (reader.CheckIfFileExists())
            {
                if (reader.CheckIfWordExists(usernameTextBox.Text))
                {
                    SystemSounds.Asterisk.Play();
                    messageTextBox.Text = "Username already exists!";
                }
                else
                {
                    saver.WriteToFile(usernameTextBox.Text);
                    messageTextBox.Text = "Username added to file on Desktop!";
                }
            }
            else
            {
                saver.WriteToFile(usernameTextBox.Text);
                messageTextBox.Text = "Username added to file on Desktop!";
            }
        }

        private void MakeFadeOut()
        {
            saveUsernameButton.IsEnabled = false;

            lbl.Visibility = Visibility.Visible;

            DispatcherTimer t = new DispatcherTimer();

            //Set the timer interval to the length of the animation.
            t.Interval = new TimeSpan(0, 0, 3);
            t.Tick += (EventHandler)delegate (object snd, EventArgs ea)
            {
                // The animation will be over now, collapse the label.
                lbl.Visibility = Visibility.Hidden;

                saveUsernameButton.IsEnabled = true;
                
                // Get rid of the timer.
                ((DispatcherTimer)snd).Stop();
            };

            t.Start();
        }

        private void playMusicButton_Click(object sender, RoutedEventArgs e)
        {
            player.PlayLooping();
        }

        private void stopMusicButton_Click(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
    }
}