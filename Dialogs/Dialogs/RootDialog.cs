using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Dialogs.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public string name;
        public long age;
        public string color;
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Text(context, this.NameReceived, "What is your name?");
        }
        private async Task NameReceived(IDialogContext context, IAwaitable<string> result)
        {
            name = await result;
            PromptDialog.Number(context, this.AgeReceived, name+", what is your age?");

        }
        private async Task AgeReceived(IDialogContext context, IAwaitable<long> result)
        {
            age = await result;
            PromptDialog.Text(context, this.ColorReceived, "Also tell me what is your fav color?");

        }
        private async Task ColorReceived(IDialogContext context, IAwaitable<string>result)
        {
            color = await result;
            context.PostAsync("hello ," + name + " your age is " + age + " and ur fav color is " + color);

        }

    }
}