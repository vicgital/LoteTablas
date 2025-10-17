using LoteTablas.Infrastructure.Configuration.Constants;
using LoteTablas.Infrastructure.Configuration.Helpers;
using MongoDB.Driver;


var config = ConfigurationHelper.GetConfiguration();

var mongoConnectionString = config[EnvironmentVariableNames.LOTETABLAS_MONGODB_CONNECTION_STRING] ?? throw new ArgumentException($"{EnvironmentVariableNames.LOTETABLAS_MONGODB_CONNECTION_STRING} was not found in App Configuration");

var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
// Set the ServerApi field of the settings object to set the version of the Stable API on the client
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
var client = new MongoClient(settings);
var database = client.GetDatabase("lotetablas");
var insertOneOptions = new InsertOneOptions();
var insertManyOptions = new InsertManyOptions();





#region LotteryTypes

var lotteryTypes = database.GetCollection<LoteTablas.Grpc.Lottery.Domain.Entities.LotteryType>("lotteryTypes");

var newLotteryType = new LoteTablas.Grpc.Lottery.Domain.Entities.LotteryType
{
    Name = "Loteria Mexicana",
    Description = "Loteria Mexicana",
    Code = "LMX"
};
await lotteryTypes.InsertOneAsync(newLotteryType, insertOneOptions, CancellationToken.None);

#endregion

#region Lottery

var lotteriesCollection = database.GetCollection<LoteTablas.Grpc.Lottery.Domain.Entities.Lottery>("lotteries");
var newLottery = new LoteTablas.Grpc.Lottery.Domain.Entities.Lottery
{
    Name = "Loteria Mexicana",
    Description = "Loteria Mexicana",
    LotteryTypeId = newLotteryType.Id,
    LotteryType = newLotteryType.Name,
    OwnerUserId = null,
    Enabled = true
};

await lotteriesCollection.InsertOneAsync(newLottery, insertOneOptions, CancellationToken.None);

#endregion

#region Cards
var cardsCollection = database.GetCollection<LoteTablas.Grpc.Lottery.Domain.Entities.Card>("cards");

List<LoteTablas.Grpc.Lottery.Domain.Entities.Card> cards = [
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Gallo",
        ImagePath= "/cards/1_el_gallo_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Diablito",
        ImagePath = "/cards/2_el_diablito_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Dama",
        ImagePath = "/cards/3_la_dama_md.jpg",
        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Catrin",
        ImagePath = "/cards/4_el_catrin_md.jpg",
        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Paraguas",
        ImagePath = "/cards/5_el_paraguas_md.jpg"
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Sirena",
        ImagePath = "/cards/6_la_sirena_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Escalera",
        ImagePath = "/cards/7_la_escalera_md.jpg",
        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Botella",
        ImagePath = "/cards/8_la_botella_md.jpg",
        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Barril",
        ImagePath = "/cards/9_el_barril_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Arbol",
        ImagePath = "/cards/10_el_arbol_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Melon",
        ImagePath = "/cards/11_el_melon_md.jpg"
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Valiente",
        ImagePath = "/cards/12_el_valiente_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Gorrito",
        ImagePath = "/cards/13_el_gorrito_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Muerte",
        ImagePath = "/cards/14_la_muerte_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Pera",
        ImagePath = "/cards/15_la_pera_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Bandera",
        ImagePath = "/cards/16_la_bandera_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Bandolon",
        ImagePath = "/cards/17_el_bandolon_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Violoncello",
        ImagePath = "/cards/18_el_violoncello_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Garza",
        ImagePath = "/cards/19_la_garza_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Pajaro",
        ImagePath = "/cards/20_el_pajaro_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Mano",
        ImagePath = "/cards/21_la_mano_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Bota",
        ImagePath = "/cards/22_la_bota_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Luna",
        ImagePath = "/cards/23_la_luna_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Cotorro",
        ImagePath = "/cards/24_el_cotorro_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Borracho",
        ImagePath = "/cards/25_el_borracho_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Negrito",
        ImagePath = "/cards/26_el_negrito_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Corazon",
        ImagePath = "/cards/27_el_corazon_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Sandia",
        ImagePath = "/cards/28_la_sandia_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Tambor",
        ImagePath = "/cards/29_el_tambor_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Camaron",
        ImagePath = "/cards/30_el_camaron_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "Las Jaras",
        ImagePath = "/cards/31_las_jaras_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Musico",
        ImagePath = "/cards/32_el_musico_md.jpg",        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Araña",
        ImagePath = "/cards/33_la_arana_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Soldado",
        ImagePath = "/cards/34_el_soldado_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Estrella",
        ImagePath = "/cards/35_la_estrella_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Cazo",
        ImagePath = "/cards/36_el_cazo_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Mundo",
        ImagePath = "/cards/37_el_mundo_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Apache",
        ImagePath = "/cards/38_el_apache_md.jpg",
        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Nopal",
        ImagePath = "/cards/39_el_nopal_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Alacran",
        ImagePath = "/cards/40_el_alacran_md.jpg",        
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Rosa",
        ImagePath = "/cards/41_la_rosa_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Calavera",
        ImagePath = "/cards/42_la_calavera_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Campana",
        ImagePath = "/cards/43_la_campana_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Cantarito",
        ImagePath = "/cards/44_el_cantarito_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Venado",
        ImagePath = "/cards/45_el_venado_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Sol",
        ImagePath = "/cards/46_el_sol_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Corona",
        ImagePath = "/cards/47_la_corona_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Chalupa",
        ImagePath = "/cards/48_la_chalupa_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Pino",
        ImagePath = "/cards/49_el_pino_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Pescado",
        ImagePath = "/cards/50_el_pescado_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Palma",
        ImagePath = "/cards/51_la_palma_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Maceta",
        ImagePath = "/cards/52_la_maceta_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "El Arpa",
        ImagePath = "/cards/53_el_arpa_md.jpg",
    },
    new LoteTablas.Grpc.Lottery.Domain.Entities.Card {
        Name = "La Rana",
        ImagePath = "/cards/54_la_rana_md.jpg",
    },
];

await cardsCollection.InsertManyAsync(cards, insertManyOptions, CancellationToken.None);

#endregion

#region LotteryCards
var lotteryCardsCollection = database.GetCollection<LoteTablas.Grpc.Lottery.Domain.Entities.LotteryCards>("lotteryCards");
LoteTablas.Grpc.Lottery.Domain.Entities.LotteryCards lotteryCards = new()
{
    LotteryId = newLottery.Id,
};

var ordinal = 1;
foreach (var card in cards)
{
    lotteryCards.Cards.Add(new LoteTablas.Grpc.Lottery.Domain.Entities.LotteryCard
    {
        CardId = card.Id,
        Ordinal = ordinal
    });
    ordinal++;
}

await lotteryCardsCollection.InsertOneAsync(lotteryCards, insertOneOptions, CancellationToken.None);

#endregion