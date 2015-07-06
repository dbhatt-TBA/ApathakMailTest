
function updateSeries(chartID, Series, SeriesLabel, chartType) {
    //debugger;
    var serie = new Array();
    var yAxisMax, yAxisMin;
    var chart = $("#" + chartID).highcharts();

    try {
        var newSeriesLength = Series.length;
        var yAxisMax, yAxisMin
        var existingSeriesLength = chart.series.length;

        if (newSeriesLength >= existingSeriesLength) {

            for (var iCtr = 0; iCtr < Series.length; iCtr++) {
                var isSeriesExists = false;

                for (var jCtr = 0; jCtr < chart.series.length; jCtr++) {
                    if (chart.series[jCtr].name == SeriesLabel[iCtr]) {
                        var Ctr = 0;
                        isSeriesExists = true;
                        chart.series[jCtr].setData(Series[iCtr], true);
                        break;
                    }
                }

                if (isSeriesExists == false) {
                    var Ctr = 0;
                    try {
                        if (SeriesLabel[Ctr].indexOf("Diff") == 0) {
                            chart.addSeries({
                                yAxis: 1,
                                name: SeriesLabel[Ctr],
                                data: Series[Ctr]
                            }, true);
                            Ctr++;
                        }
                        else {
                            chart.addSeries({
                                yAxis: 0,
                                name: SeriesLabel[iCtr],
                                data: Series[iCtr]
                            }, true);
                        }
                    }
                    catch (e) {

                    }
                }
            }
        }
        else {
            for (var iCtr = 0; iCtr < chart.series.length; iCtr++) {
                var Ctr = 0;
                var isSeriesExists = false;
                for (var jCtr = 0; jCtr < Series.length; jCtr++) {
                    if (chart.series[iCtr].name == SeriesLabel[jCtr]) {
                        isSeriesExists = true;
                        chart.series[iCtr].setData(Series[jCtr], true);
                        if (chart.series[Ctr].name.indexOf("Diff") == 0) {
                            yAxisMax = chart.yAxis[0].getExtremes().max;
                            yAxisMin = chart.yAxis[0].getExtremes().min;
                            chart.yAxis[0].setExtremes(yAxisMin, yAxisMax, true);

                            yAxisMax = chart.yAxis[1].getExtremes().max;
                            yAxisMin = chart.yAxis[1].getExtremes().min;
                            chart.yAxis[0].setExtremes(yAxisMin, yAxisMax, true);
                            Ctr++;
                        }
                        break;
                    }
                }

                if (isSeriesExists == false) {
                    chart.series[iCtr].remove(true);
                    iCtr--;
                }
            }
            chart.redraw();
        }
    }
    catch (err) {
        // alert( "updateSeries : " + err );
    }
}

function getSeries(Series, SeriesLabel) {
    var serie = new Array();
    var ctr = 0;

    try {

        for (var i = 0; i < Series.length; i++) {
            if (Series[i] != "" && SeriesLabel[i] != "") {
                serie[ctr] =
                    {
                        yaxis: 0,
                        name: SeriesLabel[i],
                        data: Series[i]
                    }
                ctr++;
            }
        }
    }
    catch (err) {
        // alert( "getSeries : " + err );
    }

    return serie;
}

function chartCreateAndUpdate(chartID, strChartTmp, chartType) {
    try {
        if (chartType == "" || chartType == "0" || chartType == null) {
            //chartType = "line";
            chartType = "";
        }

        var aryChartTmp = strChartTmp.split('^');

        var Series = new Array();
        var SeriesLabel = new Array();

        var XVal = new Array();
        var YVal = new Array();
        var ZVal = new Array();

        var labelCtr = 0;
        var seriesCtr = 0;
        var startYear;
        var pointInt = 0;
        var YAaxisName = "";

        if (aryChartTmp != "") {

            // to get  prepared series and labels
            var outputSeries = createSeries(aryChartTmp);
            Series = outputSeries[0];

            //['06/01/2014 20:14:00',182450.0000],
            //debugger;

            if (chartID == "2123_88888888") {
                //alert(Series[0]);
                for (var m = 0; m < Series[0].length; m++) {
                    //var mm = Series[0][m][0].split("/")[1] + "/" + Series[0][m][0].split("/")[0];
                    XVal[m] = Series[0][m][0]; //.split(",")[0];
                    //alert(XVal[m]);
                    YVal[m] = Series[0][m][1];
                    //alert(YVal[m]);
                }
            }
            else {

                for (var m = 0; m < Series[0].length; m++) {
                    //var mm = Series[0][m][0].split("/")[1] + "/" + Series[0][m][0].split("/")[0];
                    XVal[m] = Series[0][m][0].split("/")[1] + "/" + Series[0][m][0].split("/")[0];
                    YVal[m] = Series[0][m][1];
                }


                for (var s = 0; s < Series[1].length; s++) {
                    ZVal[s] = Series[1][s][1];
                }
            }

            for (var a = 0; a < outputSeries[1].length; a++) {
                if (outputSeries[1][a].indexOf("") == 0) {
                    SeriesLabel[a] = outputSeries[1][a];
                }
            }

            if (chartID == "vcm_calc_transactional_currenexdetailview_88888888") {
                pointInt = 171999999; //  3 * 3600 * 1000;
                YAaxisName = "FX Rate";
            }
            else if (chartID == "vcm_calc_Transactional_CurrenexDetailView_New_88888888") {
                //alert(chartID);
                pointInt = 171999999; //  3 * 3600 * 1000;
                YAaxisName = "FX Rate";
            }
            else if (chartID == "2123_88888888") {
                //alert(chartID);
                pointInt = 171999999; //  3 * 3600 * 1000;
                YAaxisName = "Trade";
            }
            else if (chartID == "2146_88888888") {
                //alert(chartID);
                pointInt = 171999999; //  3 * 3600 * 1000;
                YAaxisName = "Implied Financing Rate";
            }
            else {
                pointInt = 31 * 24 * 3600 * 1000;
                YAaxisName = "Yield";
            }

            //debugger;
            startYear = new Date(Series[0][0][0]).getFullYear();
            // create chart object and assign options of Highcharts to it
            //var chartObj = $("#" + chartID).highcharts();
            var chartObj = null;
            if (chartType == "" || chartType == "0" || chartType == null) {
                chartObj = $("#" + chartID).highcharts();
                //for (var i = 0; i < chartObj.series.length; i++) {
                //    chartObj.series[0].remove(true);
                //}
                //chartObj.redraw();
            }
            else {
                chartObj = $("#" + chartID).highcharts({ chart: { type: chartType, renderTo: 'container', } });
            }

            if (chartID == '2146_88888888') {

                chartObj = $("#" + chartID).highcharts(
                    {
                        chart: {
                            zoomType: 'xy'
                        },
                        title: {
                            text: 'Volume vs Implied Financing Rate for E-mini S&P 500'
                        },
                        xAxis: [{
                            //categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun','Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                            categories: XVal,
                            labels: {
                                rotation: 270
                            }
                        }],
                        yAxis: [{ // Primary yAxis - for Implied Rate set in value
                            labels: {
                                format: '{value}',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            title: {
                                text: 'Implied Rate',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            opposite: true

                        }, { // Secondary yAxis - Set Volume value in {value}
                            gridLineWidth: 0,
                            title: {
                                text: 'Spread Volume',
                                style: {
                                    color: Highcharts.getOptions().colors[0]
                                }
                            },
                            labels: {
                                format: '{value}',
                                style: {
                                    color: Highcharts.getOptions().colors[0]
                                }
                            }

                        }, { // Tertiary yAxis
                            gridLineWidth: 0,
                            title: {
                                text: '',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            labels: {
                                format: '{value}',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            opposite: true
                        }],
                        tooltip: {
                            shared: true
                        },
                        legend: {
                            layout: 'horizontal',
                            align: 'left',
                            x: 150,
                            verticalAlign: 'top',
                            y: 20,
                            floating: true,
                            backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                        },
                        series: [{
                            name: 'Spread Volume',
                            type: 'column',
                            yAxis: 1,
                            data: ZVal

                        }, {
                            name: 'Implied Rate',
                            type: 'line',
                            data: YVal
                        }]
                    });
            }
            else if (chartID == '2123_88888888') {
                //debugger;
                //var XValPeriod = new Array();
                ////alert(XVal.length);
                ////alert(xvals);
                //var i, j, temparray, chunk = 10;
                //for (i = 0, j = XVal.length; i < j; i += chunk) {
                //    //XValPeriod = XVal.slice(i, i + chunk);
                //    XValPeriod.push(XVal.slice(i, i + 1));
                //    // do whatever
                //}
                //alert(Math.floor(XVal.length / 10));

                //chartObj = $("#" + chartID).highcharts(
                chartObj = $("#" + chartID).highcharts(
                    {
                        chart: {
                            type: chartType,
                            renderTo: 'container',
                            zoomType: 'xy'
                        },
                        title: {
                            text: 'CME Historical Data'
                        },

                        xAxis: [{
                            //categories: XValPeriod,
                            categories: XVal,
                            labels: {
                                rotation: -55
                            },
                            tickInterval: Math.floor(XVal.length / 10) //10
                        }],

                        yAxis: [{ // Primary yAxis - for Implied Rate set in value
                            labels: {
                                format: '{value}',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            title: {
                                text: 'Trade',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            opposite: false

                        }, { // Tertiary yAxis
                            gridLineWidth: 0,
                            title: {
                                text: '',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            labels: {
                                format: '{value}',
                                style: {
                                    color: Highcharts.getOptions().colors[1]
                                }
                            },
                            opposite: false
                        }],
                        tooltip: {
                            shared: true
                        },
                        legend: {
                            layout: 'horizontal',
                            align: 'left',
                            x: 150,
                            verticalAlign: 'top',
                            y: 20,
                            floating: true,
                            backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                        },
                        series: [{
                            name: 'Trade',
                            //type: 'line',
                            type: chartType,
                            data: YVal
                        }]
                    });
            }
            else if (chartObj != undefined && chartType != '') {
                updateSeries(chartID, Series, SeriesLabel, chartType);
            }
            else {
                chartObj = $("#" + chartID).highcharts(
                {
                    chart: {
                        //type: chartType,  //Need to update chart type as per selection
                        zoomType: 'x',
                        animation: false,
                        renderTo: 'container',
                    },
                    credits: {
                        enabled: false
                    },
                    title: {
                        text: ''
                    },
                    xAxis: {
                        title: {
                            text: 'Date'
                        },
                        //  ,gridLineWidth: 2
                        labels:
                        {
                            formatter: function () {
                                var s = this.value;
                                s = '' + new Date(s).getDate() + '/' + (new Date(s).getMonth() + 1) + '/' + new Date(s).getFullYear();
                                return s;
                            }
                        }
                    },
                    yAxis: [{
                        id: 0,
                        title: {
                            text: YAaxisName
                        }
                        //, gridLineWidth: 2
                    },
                        {
                            id: 1,
                            title: {
                                text: ''
                            },
                            opposite: true
                        }],
                    tooltip: {
                        animation: false,
                        shared: true,
                        crosshairs: {
                            width: 2,
                            color: 'gray'
                        },
                        formatter: function () {
                            var s = this.x;
                            s = new Date(s).toDateString();
                            $.each(this.points, function (i, point) {
                                s = s + '<b>' + '<br/>' + point.series.name + '</b>' + ': ' + point.y;
                            });

                            return s;
                        }

                    },
                    plotOptions: {
                        line: {
                            animation: false
                        },
                        series:
                        {
                            //  compare: 'value',
                            marker: { enabled: false },
                            animation: false,
                            pointStart: Date.UTC(Series[0][0][0].split('/')[2], Series[0][0][0].split('/')[1] - 1, Series[0][0][0].split('/')[0]),
                            pointInterval: pointInt // one yr  pointStart: Date.UTC(2014, 0, 1),
                            , turboThreshold: 0
                        }
                    },
                    legend: {
                        enabled: true
                    },
                    series: getSeries(Series, SeriesLabel)
                });
            }
        }
        else {
            var chartObj = $("#" + chartID).highcharts();
            for (var iCtr = 0; iCtr < chartObj.series.length; iCtr++) {
                chartObj.series[iCtr].remove(true);
            }

        }
    }

    catch (errorVal) {
        //  alert( "chartCreateAndUpdate : " + errorVal );
    }
    finally {
        Series = null;
        SeriesLabel = null;
    }
}

function chunk(arr, len) {

    var chunks = [],
        i = 0,
        n = arr.length;

    while (i < n) {
        chunks.push(arr.slice(i, i += len));
    }

    return chunks;
}


function createSeries(aryChartTmp) {
    try {
        var arySeries = new Array();
        var arySeriesLabel = new Array();
        var labelCtr = 0;
        var seriesCtr = 0;
        var tempData = "[[-1]]";

        if (aryChartTmp != "") {
            for (var i = 0; i < aryChartTmp.length; i++) {
                if (aryChartTmp[i].indexOf('[') >= 0) {
                    if (aryChartTmp[i] != "") {
                        if (aryChartTmp[i] == "[]") {
                            arySeries[seriesCtr] = eval("(" + [[tempData]] + ')');
                        }
                        else {
                            arySeries[seriesCtr] = eval("(" + [[aryChartTmp[i]]] + ')');
                        }
                        seriesCtr++;
                    }

                }
                else {
                    if (aryChartTmp[i] != "" && aryChartTmp[i].length > 2) {
                        arySeriesLabel[labelCtr] = aryChartTmp[i];
                        labelCtr++;
                    }
                }
            }
        }
        return [arySeries, arySeriesLabel];
    }
    catch (exc) {
    }
}




