﻿using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Liedeinblendung.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Hymnals = _hymnalReader.LoadHymnals();
            VerseList = new List<Verse>() { new Verse() { Number = 1 }, new Verse() { Number = 2 }, new Verse() { Number = 3 },
                                            new Verse() { Number = 4 }, new Verse() { Number = 5 }, new Verse() { Number = 6 },
                                            new Verse() { Number = 7 }, new Verse() { Number = 8 }, new Verse() { Number = 9 }};


        }

        public ICommand AcceptCommand => new RelayCommand(OnAcceptPressed);

        public List<HymnalData> Hymnals { get; set; } = new List<HymnalData>();
            

        public string InputNumber 
        {
            get { return GetValue<string>(); }
            set 
            { 
                SetValue(value);
                CheckValid(value);            
            }
        }


        public string InputText
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string InputVers
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool NumberFalidFlag
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public List<Verse> VerseList
        {
            get { return GetValue<List<Verse>>(); }
            set { SetValue(value); }
        }


        private readonly HymnalReader _hymnalReader = new HymnalReader();
        private readonly FadeInWriter _fadeInWriter = new FadeInWriter();






        private void CheckValid(string number)
        {
            if (Hymnals.Exists(x=>x.Number == number))
            {
                NumberFalidFlag = true;
                CreateFade(number);
                return;
            }

            if (Hymnals.Exists(x => x.Number == $"{number}a"))
            {

                NumberFalidFlag = true;
                InputNumber = $"{number}a";
                CreateFade(InputNumber);
                return;
            }

            NumberFalidFlag = false;
            InputText = "";

        }

        private void OnAcceptPressed(object obj)
        {
            _fadeInWriter.WriteFade(new HymnalData() { Number = InputNumber, Name = InputText, SongVerses = InputVers });
            ClearView();
        }

        private void ClearView()
        {
            InputNumber = "";
            InputVers = "";

        }

        private void CreateFade(string number)
        {
            var hymnal = new HymnalData();
            hymnal.Number = number;
            hymnal.Name = InputText = Hymnals.Find(x => x.Number == number).Name;
            hymnal.SongVerses = InputVers;
        }


        

    }
}
