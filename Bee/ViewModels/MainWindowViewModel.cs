﻿//
// Written by A Darkins
// Date 6/8/2020
// Issue : initial
//
// logic to handle Bees status, upadte each time damage button is pressed
//

using Bee.Class;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Bee.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {


        private string _title = "Bee Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        // List of all initialied bees
        // Using Observable collection as works well with datagrid
        //
        private ObservableCollection<GeneralBee> beelist = new ObservableCollection<GeneralBee>();
        public ObservableCollection<GeneralBee> BeeList
        {
            get => beelist;
            set
            {
                SetProperty(ref beelist, value);
            }
        }

        public ICommand ExitCmd { get; private set; }
        public ICommand DamageCmd { get; private set; }
        public MainWindowViewModel()
        {
            ExitCmd = new DelegateCommand(ExecExit);
            DamageCmd = new DelegateCommand(ExecDamage);

            // Initialise 30 bees, 10 of each type
            GeneralBee newbee = new GeneralBee();
            this.BeeList = new ObservableCollection<GeneralBee>();

            for (int i = 0; i < 30; i++)
            {
                newbee = new GeneralBee();
                if (i<10) { newbee.BeeType = GeneralBee.BeeTypes.Drone; }
                if ((i >= 10 ) && (i<20)) { newbee.BeeType = GeneralBee.BeeTypes.Worker; }
                if ((i >= 20) && (i < 30)) { newbee.BeeType = GeneralBee.BeeTypes.Queen; }
                BeeList.Add(newbee);
               
            }

        }

        private void ExecExit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ExecDamage()
        {
            // damage each bee a random amount 0-80
            var generator = new Random();
            int damage = 0;

            foreach (GeneralBee bee in BeeList)
            {
                damage = generator.Next(80);
                bee.Damage(damage);
            }

        }
    }
}
