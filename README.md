# Csharp_rdnaptrans2018

<p>Een C# (Charp) Windows Forms programma t.b.v. <strong>RD NAP</strong> <-> <strong>ETRS89</strong> transformaties. Het is omzetting van de 
<a href=https://github.com/FVellinga/gm_rdnaptrans2018>SAS implementatie</a> code naar C#. Meer details zijn daar te vinden. De SAS versie transformeert ook door naar <b>WGS84</b>. De C# versie niet. </p>
<p><strong>Noot:</strong> De code is nog niet geplaatst. De zogenaamde validatie service werkt niet op de <a href=nsgi.nl>nsgi</a> site. Ik weet niet of daar iets is veranderd. Deze applicatie is van december 2020 maar nu pas plaats ik hem op github. Even uitzoeken wat daar aan de hand is. Heb geduld.</p>


# Inleiding

<p>De C# applicatie <b>RDNAPTRANS2018</b> is een C# implementatie die de geografische Nederlandse <b>RD NAP</b> (<b>R</b>ijks<b>D</b>riehoeksmeting en <b>N</b>ormaal
<b>A</b>msterdam <b>P</b>eil) coördinaten omzet, transformeert, naar <b>ETRS89</b> (<b>E</b>uropean <b>T</b>errestrial <b>R</b>eference
<b>S</b>ystem 1989), of andersom. De code is gecertificeerd en mag het handelsmerk <b>RDNAPTRANS™2018</b> voeren. Dit betekent dat deze
transformaties correct zijn als er juist gebruik wordt gemaakt van <b>c#_rdnaptrans2018</b>. <b>RD NAP</b> eenheid is in meters, <b>ETRS89</b> is in graden
en meters (de hoogte).</p>

<p><b>RDNAPTRANS™2018</b> compliant code transformeert elk punt (binnen <strong>RD NAP</strong> en <strong>ETRS89</strong> domein), welke plek op aarde dan ook. Maar buiten de zogenaamde grids kan de afwijking groot zijn en klopt er niets meer van. Dat is volkomen correct gedrag. Sommige implementaties geven
dan een waarschuwing dat je transformeert met waarden die buiten de grid liggen Deze code geeft die waarschuwing (nog) niet.</p>

<p><b>Noot:</u></b> De twee validatiebestanden nodig voor de zelfservice certificering zijn aan verandering onderhevig. De punten die er in staan veranderen. Beter is om ze van de <strong>NSGI</strong> website zelf te halen. Dan heb je altijd de laatste versie. Voor de werking van het programma heb je ze niet nodig.</p>
<p>Tot zover de inleiding. Het is ook allemaal te vinden op <a href="http://www.nsgi.nl">www.nsgi.nl</a>.</p>

# De Code

<p>Het is C# Windows Forms applicatie ontwikkelt op Visual Studio 2019, .NET Framework 4.8. De feitelijke transformatie code zit in een Class. </p>

<p>Dit is wat volstaat om een punt te transformeren van <b>ETRS89</b> naar <b>RD</b>:</br><i>
ClsRDnaptrans ObjRdNap = new ClsRDnaptrans();</br>
ObjRdNap.Load_grid_file(ObjRdNap.rdcorr2018_file, 1);</br>
ObjRdNap.Load_grid_file(ObjRdNap.nlgeo2018_file, 2);</br>
ObjRdNap.ETRS89_to_RD("52 9 22,178", "5 23 15,500", 72.6882);</i>
</p>

<p>Er moeten twee grid files (tekst bestanden) worden ingeladen. Optioneel zijn het zogenaamde zelfvalidatiebestand en de twee certificatie
validatiebestanden. Handig, want hiermee toon je aan dat deze code werkt. </p>

