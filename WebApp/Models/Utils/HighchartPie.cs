using System;
using System.Collections.Generic;

public class HighchartPie
{
    public Serie series { get; set; }

    public class Serie
    {
        public string name { get; set; }
        public string refProj { get; set; }
        public string color { get; set; }
        public double y { get; set; }
        public double percent { get; set; }
    }
}

public class HighchartBar
{
    public List<List<Point>> series { get; set; }

    public class Point
    {
        public DateTime? date { get; set; }
        public double value { get; set; }
    }

    public class RecapSapAriba
    {
        public double currentYearSap { get; set; }
        public double currentYearAriba { get; set; }
        public double currentYearTotal { get; set; }
        public double cumulatedSap { get; set; }
        public double cumulatedAriba { get; set; }
        public double cumulatedTotal { get; set; }
    }
}

public class HighChartHisto
{
    public HistoBar HistoBars { get; set; }

    public class HistoBar
    {
        public string Name { get; set; }
        public int value { get; set; }
        public string color { get; set; }
    }
}

public class HighChartHistoHoriz
{
    public HistoBar HistoBars { get; set; }

    public class HistoBar
    {
        public string name { get; set; }
        public double y { get; set; }
        public double total { get; set; }
        public double percent { get; set; }
        public string color { get; set; }
    }
}

public class Histobarsimple
{
    public bool colorByPoint { get; set; }
    public List<HighChartHistoHoriz.HistoBar> data { get; set; }
}