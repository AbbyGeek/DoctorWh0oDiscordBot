# Doctor Who Discord Bot!

Once upon a time, there was a DnD familiar who spent many years being the familiar of numerous player characters. during their travels, they learned alot about the different spells in Dnd 5e.
This bot is a reference bot for players and DMs alike. As of right now, most generic spells in the game are queriable and will return spell cards with relevant information.

## Getting Started

To get started, create an empty text document in the project folder and name it "settings.txt". Inside the document, paste in your discord bot's token.
Within the program, you'll need to edit the file path to the settings document. This path can be found in Program.cs on line 42

### Prerequisites

What things you need to install the software and how to install them

```
.net CORE 2.1
Visual Studio

```

### Installing

as of right now this program is unfinished and will have to be executed through Visual Stuido. Eventually this will be improved.

Add your bot token

```
In the main project folder, add a text file called "settings.txt". Paste your key inside the file
```

Change file path in the main program.

```
Open visual studio and in the main program, line 42, change the file path to target your new settings file.
```

run the program to bring the bot online


## Chat Commands

!spells "spell name" will return a spell card for a Dnd 5e spell. (note: spells with multiple words in the title will need to be in quotations)


## Built With

* [DnD 5e API](http://www.dnd5eapi.co/) - Dnd online database
* [Discord.NET](https://github.com/discord-net/Discord.Net) - .NET framework for discord bots


## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Authors

* **Abby West** - *Initial work* - [Github](https://github.com/abbygeek)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
