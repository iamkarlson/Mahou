using System;

namespace Mahou
{
    static class Translation
    {
        #region English
        public static string[] UIEN = new string[] {
          /*///////////////Main UI\\\\\\\\\\\\\\\\\
          /*00-03*/"View on Github","Autostart with Windows","Update","Hotkeys",
          /*04-06*/"Convert word","Convert selection","Convert line",
          /*07-10*/"CS-Switch","Re-Press","Re-Select","Switch layout by key:",
          /*11-14*/"Block CTRL","Tray icon","Cycle Mode","Emu",
          /*15-16*/"Switch between layouts","Language",
          /*17-20*/"Apply","OK","Cancel","Help",
          /*///////////////Update UI\\\\\\\\\\\\\\\\\
          /*21-23*/"Mahou update","Check for Updates","Checking...",
          /*24-28*/"Release","Version","Title","Description","Update Mahou to ",
          /*29-32*/"Downloading","Timed out...","Error","You have latest version.",
          /*33-34*/"I think you need to update...","Error occured during check...",
          /*35-35*/"Failed to get Update info, can't connent to 'github.com', check your internet connection or proxy configuration.",
          /*///////////////MoreConfigs UI\\\\\\\\\\\\\\\\\
          /*36-36*/"Use specific layout changing by Left/Right CTRLS",
          /*37-39*/"LCtrl switches to:","RCtrl switches to:","More configs",
          /*40-41*/"Symbol Ignore:","More Tries:",
          /*///////////////Tray Icon\\\\\\\\\\\\\\\\\
          /*42-44*/"Show/Hide","Exit","Mahou (魔法)\nA magical layout switcher.",
          /*///////////////Back to MoreConfigs UI\\\\\\\\\\\\\\\\\
          /*45-47*/"Display language:","Refresh rate(ms):", "Colors:",
          /*///////////////Back to Update UI\\\\\\\\\\\\\\\\\
          /*48-51*/"Proxy","Server:port","Name/Password","Your proxy not working...",
          /*///////////////Back to MoreConfigs UI\\\\\\\\\\\\\\\\\
          /*52-58*/"Font","Size:","Position:", "More", "Back","Double hotkey:","Delay:",
  	      /*59-60*/"Experimental CS-Switch+","Transparent background in language tooltip" };
        public static string[] ToolTipsEN = new string[] {
          /*///////////////Main TT\\\\\\\\\\\\\\\\\
          /*00*/"While this option enabled, [Convert word] and [Convert line] and [Convert selection with \"CS-Switch\" enabled]\n"+
          "will just cycle between all locales instead of switching between selected in settings.\nThis mode works for almost all programs.\n"+
          "If there is program in which [Convert word] or [Convert line] or [Convert selection with \"CS-Switch\" enabled] not work,\ntry with this option enabled.\n"+
          "If you have just 2 layouts(input languages) it is HIGHLY RECOMMENDED to turn it ON, and \"Emu\" too to ON.",
          /*01*/"This is current hotkey for \"Convert word\".\nPress any key to assign it, or key with modifiers(ALT,CTRL,SHIFT).",
          /*02*/"This is current hotkey for \"Convert selection\".\nPress any key to assign it, or key with modifiers(ALT,CTRL,SHIFT).",
          /*03*/"This is current hotkey for \"Convert line\".\nPress any key to assign it, or key with modifiers(ALT,CTRL,SHIFT)",
          /*04*/"Go to GitHub repository to view source or report issue.",
          /*05*/"Toggles visibility of icon in a tray.\nIf it is hidden, to show configs window hit CTRL+ALT+SHIFT+INSERT or just run Mahou.exe again.",
          /*06*/"Check for updates, and update if needed.",
          /*07*/"Blocks hotkeys that use Control,\nwhen \"Switch layout by key\" is set to Left/Right Control.",
          /*08*/"Switching layout depends on \"Cycle Mode\" & \"Emu\" options.",
          /*09*/"If this option enabled, Cycle Mode will use Emulation of Alt+Shift/Win+Space instead \"sending window message\" that changes layout.",
          /*10*/"If this option enabled, Covert selection will use layout switching.\nAll characters will be rewriten as they must.(no problems with symbols)",
          /*11*/"Only works when Cycle Mode is OFF.",
          /*12*/"If enabled, modifiers will be repressed after conversion.\nBUT if you able to release it before conversion done, modifiers will stuck.\nUse at own risk (not recommenden).",
          /*13*/"If enabled, pressing ONE space AFTER word will not clear last word.",
          /*14*/"Enabling this, will reselect text after \"Convert selection\".",
          /*15*/"Select type for Emulate change layout.\nWin+Space works only in Windows 10!!\nWin+Space also will work better in Metro apps.",
          /*///////////////MoreConfigs TT\\\\\\\\\\\\\\\\\
          /*16*/"Pressing LCtrl will switch layout to selected.",
          /*17*/"Pressing RCtrl will switch layout to selected.",
          /*18*/"Enabling this will disable LCtrl/RCtrl from \"Switch layout by key\".\nWhile this enabled pressing LCtrl/RCtrl will switch to specified layout.", 
          /*/BACK TO MAIN
          /*19*/"Emu Type",
          /*/BACK TO MoreConfigs
          /*20*/"If enabled, symbols []{};':\",./<>? will be ignored and will not be converted.\nWorks in Convert word, Convert line, Convert selection with Cylce Mode and CS-Switch enabled.\nWon't work if you have >2 layouts an Cycle Mode enabled!",
          /*21*/"Enables more tries to get selected text in Convert selection.",
          /*22*/"If enabled, mouse cursor hovers text it will display a small tip of what langauge is now using.",
          /*23*/"Speed of checking cursor type.(if it equals \"I\" )",
          /*24*/"Colors and font of small tip.(Left - foreground, Right - background)",
          /*25-26*/"Size of language tooltip.", "Position relative to mouse position.",
          /*27*/"Enables double hotkey ability,\nand makes it possible to set modifies to hotkey.",
          /*28*/"Time to wait second hotkey press(ms)", 
          /*29*/"Combines some abilities of Convert selection with enabled CS-Switch and when it disabled.(Enable CS-Switch for it to work)\nIt can:\n1.Conversion from multiple languages at once.\n2.Ignore symbols feature work in it.\n3.Auto get language of text(not all symbols(the ones that exist in both layouts) can be rightly recognized if wrong layout selected from begin)\n4.Ability to convert symbols that exist in both layouts different if change layout before conversion.",
          /*30*/"Makes background of language tooltip transparent.\nDon't forget to change text color.(because \"White\" is default)\nSome fonts may look bad, try changing to another.(\"Georgia\" or \"Palatino Linotype\" is recommend)"};
        public static string[] MessagesEN = new string[] {
          /*0-1*/"Mahou successfully updated!", "Update complete!",
          /*002*/"Press Pause(by Default) to convert last inputted word.\nPress Scroll(by Default) while selected text is focused to convert it.\nPress Shift+Pause(by Default) to convert last inputted line.\n"+
          "Press Ctrl+Alt+Shift+Insert to show Mahou main window.\nPress Ctrl+Alt+Shift+F12 to shutdown Mahou.\n\n*Note that if you typing in not of selected in settings layouts(locales/languages), conversion will switch typed text to Language 1(Ignored if Cycle Mode is ON).\n\n"+
          "**If you have problems with symbols conversion(selection) try \"switching languages (1=>2 & 2=>1)\" or \"CS-Switch\" option.\n\nHover on any control of main window for more info about it.\n\n"+
          "************WINDOWS 10 USERS WHO USE METRO APPS************\nEnable \"Cycle Mode\", \"Emu\" and set Emu type to \"Win+Space\" these settings work better for Metro apps.\n\nTo reset settings just delete Mahou.ini in Mahou folder.\n\nRegards.", 
          /*3-5*/"****Attention****","You have assigned same hotkeys for Convert word & Convert line, that is impossible!!", "Warning!",
          /*6-7*/"You have pressed just modifiers for Convert word hotkey!!","You have pressed just modifiers for Convert selection hotkey!!",
          /*8-9*/"You have pressed just modifiers for Convert line hotkey!!","You have removed selected locales, reselect." };
        #endregion
        #region Русский
        public static string[] UIRU = new string[] {
          /*///////////////Main UI\\\\\\\\\\\\\\\
          /*00-03*/"Код на Github","Автозапуск с Windows","Обновить","Горячие клавиши",
          /*04-06*/"Конверт слова","Конверт выделения", "Конверт линии",
          /*07-10*/"КВ-Ключ","Пере-наж.","Пере-выдел.","Сменить язык клавишой:",
          /*11-14*/"Игнор. CTRL","Иконка трея","Циклч. режим","Эму",
          /*15-16*/"Конверт между языками","Язык",
          /*17-20*/"Применить","OK","Отмена","Помощь",
          /*///////////////Update UI\\\\\\\\\\\\\\\
          /*21-23*/"Mahou обновления","Проверить обновления","Проверяю...",
          /*24-28*/"Релиза","Версия","Заголовок","Описание","Обновить Mahou к ",
          /*29-32*/"Загружаю","Превышено время ожидания...","Ошибка","У вас последняя версия.",
          /*33-34*/"Я думаю следует обновиться...","Произошла ошибка при проверке...",
          /*35-35*/"Не получилось получить информацию о обновлениях, не могу соединится с 'github.com', проверьте ваше соединение с интернетом или настройки прокси.",
          /*///////////////MoreConfigs UI\\\\\\\\\\\\\\\
          /*36-36*/"Использовать спец. переключение по L/R CTRL",
          /*37-39*/"LCtrl переключает в:","RCtrl переключает в:","Дополнительные настройки",
          /*40-41*/"Игнор. символов:","Больше попыток:",
          /*///////////////Tray Icon\\\\\\\\\\\\\\\
          /*42-44*/"Показать/Скрыть","Выход","Mahou (魔法)\nВолшебный переключатель раскладок.",
          /*///////////////Back to MoreConfigs UI\\\\\\\\\\\\\\\\\
          /*45-47*/"Отображ. язык:","Скор. обнов.(мс):", "Цвета:",
          /*///////////////Back to Update UI\\\\\\\\\\\\\\\\\
          /*48-51*/"Прокси","Сервер:порт","Имя/Пароль","Ваш прокси не работает...",
          /*///////////////Back to MoreConfigs UI\\\\\\\\\\\\\\\\\
          /*52-58*/"Шрифт","Размер:","Позиция:", "Еще", "Назад", "Двойные гор. клавиши:", "Ожидание:",
  	      /*59*/"Экспериментальный КВ-Ключ+","Прозрачный фон в подсказке языка" };
        public static string[] ToolTipsRU = new string[] {
          /*///////////////Main TT\\\\\\\\\\\\\\\\\
          /*00*/"Пока включена, [Конверт слова] and [Конверт линии] and [Конверт выделения с \"КВ-Ключ\" включенной]\n"+
          "будет переключать раскладку циклично, вместо переключения между выбранными в настройках.\nЭтот режим работает с бОльшим количеством програм.\n"+
          "Если есть программа в которой [Конверт слова] или [Конверт линии] или [Конверт выделения с \"КВ-Ключ\" включенной] не работают,\nто попробуйте включить эту функцию.\n"+
          "Если у вас только 2 раскладки ОЧЕНЬ РЕКОМЕНДУЕТСЯ включить ее, и \"Эму\".",
          /*01*/"Текущая горячая клавиша для \"Конверт слова\".\nНажмите любую клавишу чтобы назначить её, или клавишу с модификаторами(ALT,CTRL,SHIFT).",
          /*02*/"Текущая горячая клавиша для \"Конверт выделения\".\nНажмите любую клавишу чтобы назначить её, или клавишу с модификаторами(ALT,CTRL,SHIFT).",
          /*03*/"Текущая горячая клавиша для \"Конверт линии\".\nНажмите любую клавишу чтобы назначить её, или клавишу с модификаторами(ALT,CTRL,SHIFT).",
          /*04*/"Открыть репозиторий на GitHub чтобы посмотреть исходный код или сообщить об ошибке.",
          /*05*/"Переключает видимость иконки в трее.\nЕсли скрыта то чтобы показать главное окно нажмите CTRL+ALT+SHIFT+INSERT или просто запустите Mahou.exe опять.",
          /*06*/"Проверить обновления, и обновиться если требуется.",
          /*07 */"Игнорирует горячую клавишу которая использует Control,\nкогда \"Сменить язык клавишой\" установлен на Left/Right Control.",
          /*08*/"Тип переключения зависит от \"Циклч. режим\" и \"Эму\".",
          /*09*/"Если включеня, Циклч. режим будет испльзовать Эмуляцию нажатия Alt+Shift/Win+Space вместо \"посылания сообщения окну\" которое переключает раскладку.",
          /*10*/"Если включена, Конверт выделения будет использовать переключение раскладки.\nВсе символы будут написаны как надо.(нет проблем с символами)",
          /*11*/"Работает только если Циклч. режим выключен.",
          /*12*/"Если включена, модификаторы будут нажаты снова после конвертации.\nНО если Вы отпустите их прежде чем завершится конвертация, модификаторы залипнут.\nИспользуйте на свой риск(не рекомендуется).",
          /*13*/"Если включена, нажатие ОДНОГО Space ПОСЛЕ слова не очистит полседнее слово.",
          /*14*/"Если включена, текст будет снова выделен после \"Конверт выделения\".",
          /*15*/"Выберите тип эмуляции переключения раскладки.\nWin+Space работает только в Windows 10!!\nWin+Space также в Metro приложениях работает лучше чем Alt+Shift.",
          /*///////////////MoreConfigs TT\\\\\\\\\\\\\\\\\
          /*16*/"Нажатие LCtrl переключит раскладку в выбраную.",
          /*17*/"Pressing RCtrl переключит раскладку в выбраную.",
          /*18*/"Включение этой функции отключает LCtrl/RCtrl из \"Сменить язык клавишой\".\nПока включена, нажатие LCtrl/RCtrl будет переключать раскладку в выдбраную.", 
          /*19*/"Тип эмуляции",
          /*/BACK TO MoreConfigs
          /*20*/"Если включен, символы []{};':\",./<>? будут игнорироваться и не будут конвертированы.\nРаботает в Конверт слова, Конверт линии, Конверт выделения с Циклч. режим и КВ-Ключ включенными.\nНе будет работатб если у вас >2 раскладок и Циклч. режим включен!",
          /*21*/"Включает несколько попыток взятия выделенного текста в Конверт выделения.",
          /*22*/"Если включено, то при наведении мыши на текстовую форму будет показана маленькая подсказка о текущем языке ввода.",
          /*23*/"Скорость проверки курсора мыши.(равен ли он \"I\", при наведении на текст/текстовую форму))",
          /*24*/"Цвета и шрифт маленькой подсказки.(Слева - цвет текста, Справав - цвет фона)",
          /*25-26*/"Размер подсказки языка возле курсора.", "Позиция относительно позиции курсора.",
          /*27*/"Включает возможность двойных горячих клавиш,\nи возможность назначить только модификатор на горячую клавишу",
          /*28*/"Время ожидания второго нажатия(мс)",
          /*29*/"Совмещает некоторые возможности Конверт выделения с КВ-Ключ включенным и когда выключен.(Для работы включите КВ-Ключ)\nВозможности:\n1.Конвертирование из разных языков за 1 конверт.\n2.Игнор. символов работает здесь.\n3.Авто-распознование языка текста(не все символы(те которые есть в обеих раскладках) могут быть распознаны правильно если выбран неправильный язык изначально)\n4.Возможность конвертирование символов которые есть в обеих раскладках по разному если менять язык перед конвертацией.",
          /*30*/"Делает фон подсказки языка прозрачным.\nНе забудьте поменять цвет текста.(т.к. \"Белый\" по умолчанию)\nНекоторые шрифты могут выглядеть не очень, попробуйте поменять шрифт.(\"Georgia\" или \"Palatino Linotype\" рекоммендуется)"};
        public static string[] MessagesRU = new string[] {
          /*0-1*/"Mahou успешно обновлен!", "Обновление завершено!",
          /*002*/"Нажмите Pause(по умолчанию) для конвертации последнего введенного слова.\nНажмите Scroll(по умолчанию) пока выделенный текс в фокусе чтобы конвертивровать его.\nНажмите Shift+Pause(по умолчанию) для конвертации последней введенной линии.\n"+
          "Нажмите Ctrl+Alt+Shift+Insert чтобы показать/скрыть главное окно.\nНажмите Ctrl+Alt+Shift+F12 чтобы завершить Mahou.\n\n*Заметьте что если you вводите текст не из выбранных раскладок в настройках, то конвертация конвертирует текст в Язык 1(Не актуально если включен Циклич. режим).\n\n"+
          "**Если у Вас проблемы с символами при Конвертации выделения попробуйте \"перключить языки местами(1=>2 & 2=>1)\" или включите \"КВ-Ключ\".\n\nНаведите мышь на любой элемент главного онка чтобы узнать подробнее о нем.\n\n"+
          "************WINDOWS 10 Metro приложения************\nВключите \"Циклч. режим\", \"Эму\" и установите тип эмуляции на\"Win+Space\" эти настройки работают лучше для Metro приложений.\n\nДля Mahou.ini in Mahou folder.\n\nУдачи.", 
          /*3-5*/"****ВНИМАНИЕ****","Вы установили одну и ту же горячую клавишу для Конверт слова и Конверт линии, это невозможно!!", "Внимание!",
          /*6-7*/"Вы нажали только модификаторы для Конверт слова!!","Вы нажали только модификаторы для Конверт выделения!!",
          /*8-9*/"Вы нажали только модификаторы для Конверт линии!!","Вы убрали выбранные ранее раскладки, выберите заново." };
        #endregion
    }
}
