//--------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Omnicell Inc.">
//     Copyright (c) 2017 Omnicell Inc. All rights reserved.
//
//     Reproduction or transmission in whole or in part, in 
//     any form or by any means, electronic, mechanical, or otherwise, is 
//     prohibited without the prior written consent of the copyright owner.
// </copyright>
//--------------------------------------------------------------------------

using System.Configuration;
using System.Diagnostics;
using System.Net;

using Microsoft.CognitiveServices.SpeechRecognition;

namespace ChattyCycleCount
{
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

    using Speech;

using WpfAnimatedGif;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region -------------------- Constants and Fields --------------------
        private readonly Thickness ChatPadding = new Thickness(5);

        private readonly Thickness ComputerChatMargin = new Thickness(10, 10, 200, 10);

        private readonly Thickness HumanChatMargin = new Thickness(200, 10, 10, 10);

        private ImageAnimationController imageAnimationController;

        private FlowDocument transcript = new FlowDocument();

        private string whatTheComputerThinksTheUserIsSaying;
        #endregion

        private MicrophoneRecognitionClient micClient;
        private bool active = false;
        private int intenttype = 0;

        #region -------------------- Constructors and Destructors --------------------
        public MainWindow()
        {
            InitializeComponent();
            Width = 1280;
            Height = 950;

            Transcript = this.transcript;
            Talker = new Talker();

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            this.IntentProcessor = new IntentProcessor();
            this.CreateMicrophoneRecoClientWithIntent();
        }
        #endregion

        #region -------------------- Public Properties --------------------
        public ITalker Talker { get; set; }

        public FlowDocument Transcript
        {
            get
            {
                return this.transcript;
            }

            set
            {
                this.transcript = value;
                this.ChatTranscriptViewer.Document = this.transcript;
            }
        }

        public string WhatTheComputerThinksTheUserIsSaying
        {
            get
            {
                return this.whatTheComputerThinksTheUserIsSaying;
            }

            set
            {
                this.whatTheComputerThinksTheUserIsSaying = value;
                this.WhatTheComputerThinksTheUserIsSayingTextbox.Text = this.whatTheComputerThinksTheUserIsSaying;
            }
        }
        #endregion

        public string SubscriptionKey
        {
            get
            {
                return ConfigurationManager.AppSettings["SubscriptionKey"]; ;
        }
        }

        /// <summary>
        /// Gets the LUIS application identifier.
        /// </summary>
        /// <value>
        /// The LUIS application identifier.
        /// </value>
        private string LuisAppId
        {
            get { return ConfigurationManager.AppSettings["luisAppID"]; }
        }

        /// <summary>
        /// Gets the LUIS subscription identifier.
        /// </summary>
        /// <value>
        /// The LUIS subscription identifier.
        /// </value>
        private string LuisSubscriptionID
        {
            get { return ConfigurationManager.AppSettings["luisSubscriptionID"]; }
        }

        private string DefaultLocale
        {
            get { return "en-US"; }
        }
        public IntentProcessor IntentProcessor { get; set; }
        
        private void CreateMicrophoneRecoClientWithIntent()
        {
          
            this.micClient =
                SpeechRecognitionServiceFactory.CreateMicrophoneClientWithIntent(
                this.DefaultLocale,
                this.SubscriptionKey,
                this.LuisAppId,
                this.LuisSubscriptionID);
            // this.micClient.AuthenticationUri = this.AuthenticationUri;
            this.micClient.OnIntent += this.OnIntentHandler;

            // Event handlers for speech recognition results
            this.micClient.OnMicrophoneStatus += this.OnMicrophoneStatus;
            this.micClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            this.micClient.OnResponseReceived += this.OnMicShortPhraseResponseReceivedHandler;
            this.micClient.OnConversationError += this.OnConversationErrorHandler;
        }
        #region -------------------- Private Methods --------------------
        private void AddAndScrollToNewParagraph(Paragraph newParagraph)
        {
            newParagraph.Loaded += OnNewParagraphLoaded;
            Transcript.Blocks.Add(newParagraph);
        }

        private void AddNewComputerSaidParagraph(string whatTheComputerSays)
        {
                var computerSaysParagraph = new Paragraph
                {
                    Foreground = Brushes.PaleTurquoise,
                    TextAlignment = TextAlignment.Left,
                    Padding = this.ChatPadding,
                    Margin = this.ComputerChatMargin,
                    FontSize = 19,
                    FontWeight = FontWeights.Normal,
                    FontStretch = FontStretches.SemiExpanded,
                    FontFamily = new FontFamily("Arial")
                };
                computerSaysParagraph.Inlines.Add(new Run(whatTheComputerSays));
            AddAndScrollToNewParagraph(computerSaysParagraph);
        }

        private void AddNewHumanSaidParagraph(string whatTheHumanSaid)
        {
            var humanSaysParagraph = new Paragraph
            {
                Foreground = Brushes.BurlyWood,
                TextAlignment = TextAlignment.Right,
                Padding = this.ChatPadding,
                Margin = this.HumanChatMargin,
                FontSize = 19,
                FontWeight = FontWeights.Normal,
                FontStretch = FontStretches.SemiExpanded,
                FontFamily = new FontFamily("Arial")
            };
            humanSaysParagraph.Inlines.Add(new Bold(new Run(whatTheHumanSaid)));
            AddAndScrollToNewParagraph(humanSaysParagraph);
        }

        private void ClearTranscript()
        {
            Transcript.Blocks.Clear();
        }

        private void Say(string whatToSay)
        {
            Talker.Say(whatToSay);
        }

        private void TheComputerSays(string whatTheComputerSays)
        {
            try
            {
                AddNewComputerSaidParagraph(whatTheComputerSays);

                Say(whatTheComputerSays);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An exception has occurred. Exception message:\n\n{ex.Message}");
            }
        }

        private void TheHumanSaid(string whatTheHumanSaid)
        {
            try
            {
                AddNewHumanSaidParagraph(whatTheHumanSaid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An exception has occurred. Exception message:\n\n{ex.Message}");
            }
        }
        #endregion

        #region -------------------- EventHandlers --------------------
        private void OnAnimationLoaded(object sender, RoutedEventArgs e)
        {
            this.imageAnimationController = ImageBehavior.GetAnimationController(this.HearththrobAnimation);
            this.imageAnimationController?.Pause();
        }

        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            this.micClient.StartMicAndRecognition();
            // StartButton.IsEnabled = false;
        }
        private void OnMuteButtonClick(object sender, RoutedEventArgs e)
        {
            this.micClient.EndMicAndRecognition();
            // StartButton.IsEnabled = true;
        }

        private void OnResetButtonClick(object sender, RoutedEventArgs e)
        {
            ClearTranscript();
            CreateMicrophoneRecoClientWithIntent();
            this.micClient.EndMicAndRecognition();
            WhatTheComputerThinksTheUserIsSaying = string.Empty;
            StartButton.IsEnabled = true;
            this.imageAnimationController?.Pause();
            this.active = false;

        }
        
        private void OnClickButtonStartAnimation(object sender, RoutedEventArgs e)
        {
            this.imageAnimationController?.Play();
        }

        private void OnClickButtonStopAnimation(object sender, RoutedEventArgs e)
        {
            this.imageAnimationController?.Pause();
        }

        private void OnNewParagraphLoaded(object sender, RoutedEventArgs e)
        {
            var paragraph = (Paragraph)sender;
            paragraph.Loaded -= OnNewParagraphLoaded;
            paragraph.BringIntoView();
        }
        
        #endregion

        #region copiedfromsample

        /// <summary>
        /// Called when a final response is received and its intent is parsed
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechIntentEventArgs"/> instance containing the event data.</param>
        private void OnIntentHandler(object sender, SpeechIntentEventArgs e)
        {
            this.micClient.EndMicAndRecognition();
            intenttype = IntentProcessor.ProcessIntent(e.Payload, WriteUserMessage, WriteSystemMessage, WriteIntent);
            
            this.Dispatcher.Invoke(() =>
            {
                if (intenttype == 1)
                {
                    active = true;
                    imageAnimationController?.Play();
                }
                if (intenttype == 2)
                {
                    active = false;
                    imageAnimationController?.Pause();
                    this.micClient.EndMicAndRecognition();
                    return;
                }
                this.micClient.EndMicAndRecognition();
                // this.micClient.StartMicAndRecognition();
            });
        }

        private void OnMicShortPhraseResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                // active = false;
                this.micClient.EndMicAndRecognition();
            }));
        }

        /// <summary>
        /// Called when a partial response is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PartialSpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            Dispatcher.Invoke(
                () =>
                {
                    WhatTheComputerThinksTheUserIsSaying = e.PartialResult;
                });
        }

        /// <summary>
        /// Called when an error is received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechErrorEventArgs"/> instance containing the event data.</param>
        private void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                //TODO 
                // _startButton.IsEnabled = true;
                // _radioGroup.IsEnabled = true;
            });

          
        }

        /// <summary>
        /// Called when the microphone status has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="MicrophoneEventArgs"/> instance containing the event data.</param>
        private void OnMicrophoneStatus(object sender, MicrophoneEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                StartButton.IsEnabled = !e.Recording;
                if (e.Recording)
                {
                    this.imageAnimationController?.Play();
                }
                else
                {
                    this.imageAnimationController?.Pause();
                }
            });
        }

        

        private void WriteSystemMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                //if (active || intenttype == 1 || intenttype == 2 )
                //{
                    this.imageAnimationController?.Play();
                    TheComputerSays(message);
                    this.imageAnimationController?.Pause();
                //}
            });
        }

        private void WriteUserMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
              TheHumanSaid(message);
            });
        }

        private void WriteIntent(string message)
        {
            Dispatcher.Invoke(() =>
            {
               // IntentTxt.Text = message;
            });
        }
        #endregion
    }
}