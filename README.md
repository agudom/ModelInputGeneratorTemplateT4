T4 template to read CSV and generate ModelInput for ML.net.

Set parameters and that´s all!

    /* Parameters */
    string fileInCSV = this.Host.ResolvePath("Data/people-100.csv");
    string nameSpace = "InputModelNameSpace";
    string className = "InputModel";    
    char chSeparator = ',';

It also includes a console application to generate the result on screen.
