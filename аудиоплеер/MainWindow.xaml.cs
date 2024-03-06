using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace аудиоплеер
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int rndm = 0;
        bool prov = false;
        int i = 0;
        int k = 1;
        int j = 0;
        int proverk;
        string[] miziks;
        string[] randoming;
        string[] randoming1;
        int second=0;
        int minutes = 0;
        List<string> hisrtory = new List<string> { } ?? new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            media.Volume = 0.01;
            Thread tread = new Thread(_ =>
            {
                while (true)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        if (media.HasAudio)
                        {
                            slider_time.Value = media.Position.Duration().Ticks;
                            if (media.Position.Duration() == media.NaturalDuration.TimeSpan)
                            {
                                if (k % 2 == 0)
                                {
                                    play_music(j);
                                }
                                else
                                {
                                    j++;
                                    slider_time.Value = 0;
                                    if (j > miziks.Length - 1)
                                    {
                                        j = 0;
                                        play_music(j);
                                    }
                                    else
                                    {
                                        play_music(j);
                                    }
                                }

                                
                            }
                        }
                    }));
                    
                    Thread.Sleep(1000);
                }
            });
            tread.Start();
        }

        public void opendirect_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker =true};
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                miziks = Directory.GetFiles(dialog.FileName);
                randoming1 = Directory.GetFiles(dialog.FileName);
                foreach (string file in miziks)
                {
                    if (file.Contains(".mp3") || file.Contains(".m4a")|| file.Contains(".wav"))
                    {
                        list.Items.Add(file + "\n");
                    }              
                }
                vrema();
                play_music(j);
            }
        }
        private void vrema()
        {
            Thread tread2 = new Thread(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(new Action(() =>
                    {
                        
                        vremka.Text = $"{media.Position.Minutes}:{media.Position.Seconds}";
                    }
                    ));
                    Thread.Sleep(1000);
                }

            });
            tread2.Start();

        }
        private void play_music(int j)
        {
            if (prov==true)
            {
                media.Source = new Uri(randoming[j]);
                string gg = Convert.ToString(randoming[j]);
                hisrtory.Add(randoming[j]);
                media.Play();
            }
            else
            {               
                media.Source = new Uri(miziks[j]);
                string gg = Convert.ToString(miziks[j]);
                media.Play();
                hisrtory.Add(miziks[j]);
            }
            
        }

        private void slider_time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Position = new TimeSpan(Convert.ToInt64(slider_time.Value));
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            slider_time.Maximum = media.NaturalDuration.TimeSpan.Ticks;
            media.Volume = slider_value.Maximum;
            
        }

        private void slider_value_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            media.Volume = slider_value.Value;
        }

        private void Button_Click_pause(object sender, RoutedEventArgs e)
        {
            if (i % 2 == 0)
            {
                media.Pause();
            }
            else
            {
                media.Play();
            }
            i++;
        }
        private void nazad_Click(object sender, RoutedEventArgs e)
        {
            if (k % 2 == 0)
            {
                play_music(j);
            }
            j--;
            if (j >= 0)
            {
                play_music(j);
            }
            else
            {
                j = miziks.Length - 1;
                play_music(j);
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (global::System.Int32 j = 0; j <miziks.Length; j++)
            {
                string lists1 = Convert.ToString(list.SelectedItem);
                string element = Convert.ToString(miziks[j]);
                if (lists1 == element + "\n")
                {
                    play_music(j);
                }
            }
        }

        private void vpered_Click(object sender, RoutedEventArgs e)
        {
            if (k % 2 == 0)
            {
                play_music(j);
            }
            j++;
            if (j > miziks.Length-1)
            {
                j = 0;
                play_music(j);
            }
            else
            {
                play_music(j);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            k++;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            randoming = randoming1;
            if (rndm%2==0)
            {
                Random rand = new Random();
                for (int i = randoming.Length - 1; i >= 0; i--)
                {
                    int kk = rand.Next(i, randoming.Length-1);                  
                    var temp = randoming[kk];
                    randoming[kk] = randoming[i];
                    randoming[i] = temp;
                }
                list.Items.Clear();
                foreach (string file in randoming)
                {
                    if (file.Contains(".mp3") || file.Contains(".m4a") || file.Contains(".wav"))
                    {
                        list.Items.Add(file + "\n");
                    }
                }
                prov = true;
            }
            else
            {
                list.Items.Clear();
                foreach (string file in miziks)
                {
                    if (file.Contains(".mp3") || file.Contains(".m4a") || file.Contains(".wav"))
                    {
                        list.Items.Add(file + "\n");
                    }
                }
                prov = false;
            }
            j=0;
            play_music(j);
            rndm++;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            media.Stop();
            History window = new History(hisrtory);
            window.ShowDialog();
            list.SelectedItem = window.list1;
            
        }
    }
}
