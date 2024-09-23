using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grimoire.Game;
using Grimoire.Networking;
using Grimoire.UI;
using System.Threading.Tasks;
using Grimoire.Networking.Handlers;

namespace Grimoire.Botting.Commands.Misc
{
    public class CmdAddSpecialHandler : RegularExpression, IBotCommand
    {
        private static IJsonMessageHandler _currentJsonHandler;
        private static IXtMessageHandler _currentXtHandler;

        public IJsonMessageHandler SpecialJsonHandler;
        public IXtMessageHandler SpecialXtHandler;
        public Action ActionW { get; set; }
        public string Label { get; set; }
        public enum Action
        {
            ADD,
            STOP
        }

        public async Task Execute(IBotEngine instance)
        {
            switch (ActionW)
            {
                case Action.ADD:
                    UnregisterHandlers();
                    RegisterHandler();
                    break;
                case Action.STOP:
                    UnregisterHandlers();
                    break;
            }
        }

        private void RegisterHandler()
        {
            switch (Label)
            {
                case "Auto Zone - Ultradage":
                    SpecialJsonHandler = new HandlerAutoZoneUltraDage();
                    break;
                case "Auto Zone - Dark Carnax":
                    SpecialJsonHandler = new HandlerAutoZoneDarkCarnax();
                    break;
                case "Auto Zone - Astral Empyrean":
                    SpecialJsonHandler = new HandlerAutoZoneAstralEmpyrean();
                    break;
                case "Auto Zone - Queen Iona":
                    SpecialJsonHandler = new HandlerAutoZoneQueenIona();
                    break;
                case "Auto Zone - The First Speaker":
                    SpecialJsonHandler = new HandlerAutoZoneTheFirstSpeaker();
                    break;
            }
            if (SpecialJsonHandler != null)
            {
                Proxy.Instance.RegisterHandler(SpecialJsonHandler);
                _currentJsonHandler = SpecialJsonHandler;
            }
        }

        public static void UnregisterHandlers()
        {
            if (_currentJsonHandler != null)
            {
                Proxy.Instance.UnregisterHandler(_currentJsonHandler);
                _currentJsonHandler = null;
            }
            if (_currentXtHandler != null)
            {
                Proxy.Instance.UnregisterHandler(_currentXtHandler);
                _currentXtHandler = null;
            }
        }

        public override string ToString()
        {
            string text = "";
            switch (ActionW)
            {
                case Action.ADD:
                    text = $"Start: {Label}";
                    break;
                case Action.STOP:
                    text = $"Stop: {Label}";
                    break;
            }
            return text;
        }
    }
}