<?

//This gets the data for the version from the unity and puts it in $ver
$ver = $_POST['version'];
//This gets the data for the security code from the unity and puts it in $hash
$hash = $_POST['hash'];

//this checks if the $hash is equal to abcd , if not this stops the script
if ($hash == "abcd")
{
	//if it does equal to abcd then it checks
	//if the $ver equals to 0.0.1 , if so then returns to unity up-to-date
	//if not then it returns Update Needed to unity
	
	if ($ver == "0.0.1")
		echo "up-to-date";
	else
		echo "Update Needed";
}

?>