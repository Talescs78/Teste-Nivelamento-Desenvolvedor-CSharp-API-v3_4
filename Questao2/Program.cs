using Newtonsoft.Json;
using System.Net.Http.Json;
using Questao2;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        string ParamURL = "";
        int i = 0;
        int TotalPaginas = 0;
        int TotalGols = 0;

        for (int j = 1; j <= 2; j++)
        {
            ParamURL = "/api/football_matches?year=" + year + "&team" + j + "=" + team;

            HttpClient client = new HttpClient();
            Uri DadosUri;

            client.BaseAddress = new Uri("https://jsonmock.hackerrank.com");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpResponseMessage response = client.GetAsync(ParamURL).Result;

            if (response.IsSuccessStatusCode)
            {
                DadosUri = response.Headers.Location;
                RetornoAPI retornoAPI = new RetornoAPI();
                retornoAPI = response.Content.ReadFromJsonAsync<RetornoAPI>().Result;

                TotalPaginas = retornoAPI.total_pages;

                for (i = 1; i <= TotalPaginas; i++)
                {
                    ParamURL = "/api/football_matches?year=" + year + "&team" + j + "=" + team + "&page=" + i;
                    HttpClient client1 = new HttpClient();
                    Uri DadosUri1;

                    client1.BaseAddress = new Uri("https://jsonmock.hackerrank.com");
                    client1.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    System.Net.Http.HttpResponseMessage responseDetalhes = client1.GetAsync(ParamURL).Result;

                    if (responseDetalhes.IsSuccessStatusCode)
                    {
                        DadosUri1 = responseDetalhes.Headers.Location;
                        RetornoAPI retornoAPI1 = new RetornoAPI();
                        retornoAPI1 = responseDetalhes.Content.ReadFromJsonAsync<RetornoAPI>().Result;

                        for (int Contador = 0; Contador <= retornoAPI1.data.Length - 1; Contador++)
                        {
                            if (j == 1)
                            {
                                TotalGols += Convert.ToInt32(retornoAPI1.data[Contador].team1goals);
                            }
                            else
                            {
                                TotalGols += Convert.ToInt32(retornoAPI1.data[Contador].team2goals);
                            }
                        }
                    }
                }
            }
        }
        return TotalGols;
    }
}