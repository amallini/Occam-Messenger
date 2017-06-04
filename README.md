# Occam-Messenger
The SIMPLEST and most reliable messenger available.

See English version below.

Кто не знает - Бритва Оккама - это правило, которое гласит "Не вводи новых сущностей без необходимости".

Данный проект называется Оккам потому что для передачи сообщений не используется какой-либо мессенджерский протокол (AIM, XMPP и т.п.)
Согласитесь, это здорово, потому что не надо тратить время на их изучение, искать готовые NuGet пакеты и разбираться еще и в них.
Внезапно (!) оказалось, что для практически мгновенной передачи сообщений можно использовать общие папки (shared folders) облачных сервисов типа DropBox. Еще это здорово потому что данный способ передачи сообщений практически невозможно запретить.

Все что нужно - это зарегистрироваться в DropBox (или его конкуренте), скачать Windows клиент, создать shared folder и послать приглашение абоненту. Абонент должен приглашение принять, если у него еще нет DropBox - то зарегистрироваться и установить Windows клиента. В результате у вас и у абонента появится физическая папка с магическим свойством - стоит положить в нее файл, как тут же он появляется в папке абонента и наоборот. DropBox синхронизирует с задержкой 5-6 сек. Это хорошо, но долго. MS OneDrive синхронизирует примерно за 3 сек., а pCloud за 1.5 и это пока лучшее из того что я протестировал.

После этого передача сообщения сводится к простому копированию файла с этим сообщением в общую папку.

Получить сообщение очень просто - нужно по таймеру опрашивать эту папку и если там что-то появилось - читать файл и извлекать из него сообщение. Ну и удалять файл после этого чтобы не захламлять окружающую среду.

Кроме собственно сообщений нужно передавать статусы. Это делается с помощью передачи служебных сообщений (команд). Отличать сообщение от команды можно простым делимитером - например точкой с запятой.
В данной программе в файле может быть либо текст, либо команда. Первый символ в файле либо T (text) либо С (command)
Дадее идет точка с запятой и тело сообщения.
Для текста - это собственно текст сообщения.
Для команды - это имя команды и через двоеточие параметры команды
Например Status:On
Для того чтобы каждый из абонентов читал только сообщения, адресованные ему, примем соглашение что файлы от первого ко второму будут иметь имя вида P_<Number> (P = Primary), а обратно будут иметь имя вида S_<Number>  (S = Secondary).

Для передачи файлов используется та же общая папка.
Файл будет иметь произвольное имя.
Чтобы послать файл абонент копирует его в папку и шлет служебное сообщение вида 
C;F:<имя файла>
Получатель получает служебное сообщение и ждет когда в папке образуется переданный файл.
После его появления он перемещает этот файл в предопределенную папку и все.

Из сторонних пакетов используется только SQLite (для хранения данных о каналах) и NAudio для проигрывания аудиофайлов.

Кроме передачи состояний (Здесь/Отключен) и передачи текста, файлов и голосовых файлов в программе ничего нет.
Вернее есть еще режим конференции.

Больше никаких свистелок и перделок.
Хотите - добавляйте.

Удачи.

-------------------------------------------------------------------------------------------------------------------------------

English version

Who knows - Occam's Razor is a rule that States "Not to introduce new entities without necessity."
This project is called Occam because for messaging I don't use any special protocol like AIM or XMPP.
Agree, it's great because you don't have to spend time to study them, to look for a ready NuGet packages and to deal with them.
Suddenly (!) it turned out that for almost instant transfer of messages, you can use public folders of cloud services like DropBox.
It's still great because this method of transmitting messages almost impossible to ban.

All you need is to register with DropBox (or its competitor), download Windows client, create a shared folder and send an invitation to the subscriber. The subscriber has the invitation to accept, if he does not have DropBox, then register and install the Windows client. As a result, you will have a physical folder with the magical ability to sync files. DropBox has a delay of 5-6 seconds. MS OneDrive for about 3 seconds, and pCloud has a delay of 1.5 ыусщтвы and this is the best of what I tested.

After that, the transmission of a message is as simple as copying the file with this message in a public folder.

To receive a message is very simple - you need a timer to poll the folder and if there is something out there - to read a file and extract from it a message.

The program can only to transfer text, files and status. It allows conference. That's all. Nothing more. It's Occam.
If you want add what do you want. It's open source.

Good luck!


