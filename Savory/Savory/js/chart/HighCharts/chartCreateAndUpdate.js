
function updateSeries( chartID, Series, SeriesLabel )
{
    var serie = new Array();
    var yAxisMax, yAxisMin;

    var chart = $( "#" + chartID ).highcharts();

    try
    {
        var newSeriesLength = Series.length;
        var yAxisMax, yAxisMin
        var existingSeriesLength = chart.series.length;

        if ( newSeriesLength > existingSeriesLength )
        {
            for ( var iCtr = 0; iCtr < Series.length; iCtr++ )
            {
                var isSeriesExists = false;

                for ( var jCtr = 0; jCtr < chart.series.length; jCtr++ )
                {
                    if ( chart.series[jCtr].name == SeriesLabel[iCtr] )
                    {
                        var Ctr = 0;
                        isSeriesExists = true;
                        chart.series[jCtr].setData( Series[iCtr], true );
                        break;
                    }
                }

                if ( isSeriesExists == false )
                {
                    var Ctr = 0;
                    try
                    {
                        if ( SeriesLabel[Ctr].indexOf( "Diff" ) == 0 )
                        {
                            chart.addSeries( {
                                yAxis: 1,
                                name: SeriesLabel[Ctr],
                                data: Series[Ctr]
                            }, true );
                            Ctr++;
                        }
                        else
                        {
                            chart.addSeries( {
                                yAxis: 0,
                                name: SeriesLabel[iCtr],
                                data: Series[iCtr]
                            }, true );
                        }
                    }
                    catch ( e )
                    {

                    }
                }
            }
        }
        else
        {
            for ( var iCtr = 0; iCtr < chart.series.length; iCtr++ )
            {
                var Ctr = 0;
                var isSeriesExists = false;
                for ( var jCtr = 0; jCtr < Series.length; jCtr++ )
                {
                    if ( chart.series[iCtr].name == SeriesLabel[jCtr] )
                    {
                        isSeriesExists = true;
                        chart.series[iCtr].setData( Series[jCtr], true );
                        if ( chart.series[Ctr].name.indexOf( "Diff" ) == 0 )
                        {
                            yAxisMax = chart.yAxis[0].getExtremes().max;
                            yAxisMin = chart.yAxis[0].getExtremes().min;
                            chart.yAxis[0].setExtremes( yAxisMin, yAxisMax, true );

                            yAxisMax = chart.yAxis[1].getExtremes().max;
                            yAxisMin = chart.yAxis[1].getExtremes().min;
                            chart.yAxis[0].setExtremes( yAxisMin, yAxisMax, true );
                            Ctr++;
                        }
                        break;
                    }
                }

                if ( isSeriesExists == false )
                {
                    chart.series[iCtr].remove( true );
                    iCtr--;
                }
            }
            chart.redraw();
        }
    }
    catch ( err )
    {
        // alert( "updateSeries : " + err );
    }
}
function getSeries( Series, SeriesLabel )
{
    var serie = new Array();
    var ctr = 0;

    try
    {

        for ( var i = 0; i < Series.length; i++ )
        {
            if ( Series[i] != "" && SeriesLabel[i] != "" )
            {
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
    catch ( err )
    {
        // alert( "getSeries : " + err );
    }


    return serie;
}

function chartCreateAndUpdate( chartID, strChartTmp )
{
    try
    {
        var aryChartTmp = strChartTmp.split( '^' );

        var Series = new Array();
        var SeriesLabel = new Array();

        var labelCtr = 0;
        var seriesCtr = 0;
        var startYear;
        var pointInt = 0;
        var YAaxisName="";

        if ( aryChartTmp != "" )
        {
            // to get  prepared series and labels
            var outputSeries = createSeries( aryChartTmp );
            Series = outputSeries[0];

            for ( var a = 0; a < outputSeries[1].length; a++ )
            {
                if ( outputSeries[1][a].indexOf( "" ) == 0 )
                {
                    SeriesLabel[a] = outputSeries[1][a];
                }
            }

            if ( chartID == "vcm_calc_transactional_currenexdetailview_88888888" )
            {
                pointInt = 171999999; //  3 * 3600 * 1000;
                YAaxisName="FX Rate";
            }
            else
            {
                pointInt = 31 * 24 * 3600 * 1000;
                YAaxisName="Yield";
            }


            startYear = new Date( Series[0][0][0] ).getFullYear();
            // create chart object and assign options of Highcharts to it
            var chartObj = $( "#" + chartID ).highcharts();
            if ( chartObj != undefined )
            {
                updateSeries( chartID, Series, SeriesLabel );
            }
            else
            {
                chartObj = $( "#" + chartID ).highcharts( 
                {
                    chart: {

                        zoomType: 'x',
                        animation: false,
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
                            formatter: function ()
                            {
                                var s = this.value;
                                s = '' + new Date( s ).getDate() + '/' + ( new Date( s ).getMonth() + 1 ) + '/' + new Date( s ).getFullYear();
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
                        formatter: function ()
                        {
                            var s = this.x;
                            s = new Date( s ).toDateString();
                            $.each( this.points, function ( i, point )
                            {
                                s = s + '<b>' + '<br/>' + point.series.name + '</b>' + ': ' + point.y;
                            } );

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
                            pointStart: Date.UTC( 2013, 0, 1 ),
                            pointInterval: pointInt // one yr
                            , turboThreshold: 0
                        }
                    },
                    legend: {
                        enabled: true
                    },
                    series: getSeries( Series, SeriesLabel )
                } );
            }
        }
        else
        {
            var chartObj = $( "#" + chartID ).highcharts();
            for ( var iCtr = 0; iCtr < chartObj.series.length; iCtr++ )
            {
                chartObj.series[iCtr].remove( true );

            }

        }
    }

    catch ( errorVal )
    {
        //  alert( "chartCreateAndUpdate : " + errorVal );
    }
    finally
    {
        Series = null;
        SeriesLabel = null;
    }
}

function createSeries( aryChartTmp )
{
    try
    {
        var arySeries = new Array();
        var arySeriesLabel = new Array();
        var labelCtr = 0;
        var seriesCtr = 0;
        var tempData = "[[-1]]";

        if ( aryChartTmp != "" )
        {
            for ( var i = 0; i < aryChartTmp.length; i++ )
            {
                if ( aryChartTmp[i].indexOf( '[' ) >= 0 )
                {
                    if ( aryChartTmp[i] != "" )
                    {
                        if ( aryChartTmp[i] == "[]" )
                        {
                            arySeries[seriesCtr] = eval( "(" + [[tempData]] + ')' );
                        }
                        else
                        {
                            arySeries[seriesCtr] = eval( "(" + [[aryChartTmp[i]]] + ')' );
                        }
                        seriesCtr++;
                    }

                }
                else
                {
                    if ( aryChartTmp[i] != "" && aryChartTmp[i].length > 2 )
                    {
                        arySeriesLabel[labelCtr] = aryChartTmp[i];
                        labelCtr++;
                    }
                }
            }
        }
        return [arySeries, arySeriesLabel];
    }
    catch ( exc )
    {
    }
}




