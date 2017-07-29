<?php
mb_internal_encoding("UTF-8");
$enlace =  mysqli_connect("localhost", "id2386751_faherian", "fontanar", "id2386751_guybotdata");

if( (mysqli_connect_errno())){
	printf("Conexión fallida: %s\n", mysql_connect_error());
	exit();
}

mysqli_set_charset($enlace, "utf8");

if (isset($_GET['Name']) && isset($_GET['Puntuation']) && isset($_GET['hash']))
{

$nuevoNombre = mysqli_real_escape_string($enlace, $_GET['Name']);
$nuevaPuntuacion = mysqli_real_escape_string($enlace, $_GET['Puntuation']);
$hash = $_GET['hash'];
	echo "nada0";	
	echo $hash;
	echo $nuevoNombre;
	echo $nuevaPuntuacion;
$Jugadores="SELECT * FROM PlayerRanking";
$resultadoJugadores = mysqli_query($enlace, $Jugadores);
$NumJugadores = mysqli_num_rows($resultadoJugadores);

$claveSecreta="faherian";

$NombreRepetido="SELECT * FROM PlayerRanking WHERE Name = '$nuevoNombre'";
$resultado = mysqli_query($enlace, $NombreRepetido);
$NumRepetidos = mysqli_num_rows($resultado);

$realhash = md5($nuevoNombre . $nuevaPuntuacion . $claveSecreta);
echo "   ";
echo $realhash;
$JugadorMinimo="SELECT * FROM PlayerRanking ORDER BY Puntuation ASC LIMIT 1";


//if(real_hash == hash) {
echo "nada1";
	if($resultadoJugadorMinimo=mysqli_query($enlace, $JugadorMinimo))
	{
		$EliminadoRepe = "DELETE FROM PlayerRanking WHERE Name = '$nuevoNombre'";
		$consultaRepe = "insert into PlayerRanking values ('$nuevoNombre', '$nuevaPuntuacion');";
		if( (mysqli_query($enlace, $EliminadoRepe)) && (mysqli_query($enlace, $consultaRepe)))
				echo "0";
	}

	else
	{
		echo "JUGADORMIN";
		echo $numminvalue;
		if($NumJugadoresMinimos>0)
		{

			if($NumJugadores<50)
			{
				$consulta = "insert into PlayerRanking values ('$nuevoNombre', '$nuevaPuntuacion');";
				if ((mysqli_query($enlace, $consulta)))
					echo"1";
				else
					echo "Error :" . mysqli_error($enlace);
			}
			else if( $nuevaPuntuacion > mysqli_fetch_assoc($resultadoJugadorMinimo['Puntuation']))
			{
				$Eliminado = "DELETE FROM PlayerRanking ORDER BY Puntuation ASC LIMIT 1";
				$consulta = "insert into PlayerRanking values ('$nuevoNombre', '$NuevaPuntuacion')";

				if((mysqli_query($enlace, $consulta)) && (mysqli_query($enlace, $Eliminado)))
					echo "2";
				else
					echo "Error :" - mysqli_error($enlace);
			}
			else
				echo "3";
			
		}
		else
		{
			echo "nadadinO";
		}

	}
//}
}
?>