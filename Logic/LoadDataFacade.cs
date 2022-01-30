using dmahnet_zadaca_3.LoadData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmahnet_zadaca_3.Logic
{
    public sealed class LoadDataFacade
    {
        private static readonly LoadDataFacade instance = new LoadDataFacade();
        private LoadClubs loadClubs = new LoadClubs();
        private LoadPlayers loadPlayers = new LoadPlayers();
        private LoadGames loadGames = new LoadGames();
        private LoadTeams loadTeams = new LoadTeams();
        private LoadEvents loadEvents = new LoadEvents();

        static LoadDataFacade()
        {
        }

        private LoadDataFacade()
        {
        }

        public static LoadDataFacade Instance
        {
            get
            {
                return instance;
            }
        }

        public void UcitajKlubove(string path)
        {
            loadClubs.Ucitaj(path);
        }

        public void UcitajIgrače(string path)
        {
            loadPlayers.Ucitaj(path);
        }

        public void UcitajUtakmice(string path)
        {
            loadGames.Ucitaj(path);
        }
        public void UcitajSastave(string path)
        {
            loadTeams.Ucitaj(path);
        }
        public void UcitajDogadaje(string path)
        {
            loadEvents.Ucitaj(path);
        }
    }
}
