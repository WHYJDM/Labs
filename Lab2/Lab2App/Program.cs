using Lab2Library;


const string FileName = "watches.xml";

var watchManager = new WatchManager();
var xmlHandler = new XmlHandler();
var menuHandler = new MenuHandler(watchManager, xmlHandler, FileName);

menuHandler.Run();
