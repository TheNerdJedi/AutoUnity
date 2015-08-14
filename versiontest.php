<?

// Gets data for the version from Unity and sets it as $ver
$ver = $_POST['version'];
// Gets the data for the security code from Unity and sets it as $hash
$hash = $_POST['hash'];


//this checks if the $hash is the same, if not this stops the script
if ($hash == "abcd")
// if it is equal then it checks for the version
{
	// if the $ver is equal to current then returns to Unity "up-to-date"
	if ($ver == "0.0.1")
		echo "up-to-date";
	
	// if not then returns "Update Needed" to Unity
	else
		echo "Update Needed";
}

?>