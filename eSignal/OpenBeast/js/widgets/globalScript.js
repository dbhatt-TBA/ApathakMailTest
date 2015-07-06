function ReplaceAll(Source, stringToFind, stringToReplace) {

    var temp = Source;

    var index = temp.indexOf(stringToFind);

    while (index != -1) {

        temp = temp.replace(stringToFind, stringToReplace);

        index = temp.indexOf(stringToFind);

    }

    return temp;

}


