To host message security service in IIS:

Buy or create a free Certificate:
select Root
Featuress View -> Server Certificates -> Create Self-Signed Certificate
the Certificate IIS craeted will store in:
	storeLocation="LocalMachine"
	storeName="My"

To view Certificate: type mmc in Start search box, select File -> Add Snap-In -> Cerificates -> Add -> Computer Account

Change App pool Identity to LocalSystem
select Advanced Setting -> Identity -> Built-In accounts -> LocalSystem

Need Http binding to run, if only Https binding will get message:
"Could not find a base address that matches scheme http for the endpoint with binding WSHttpBinding. Registered base address schemes are [https]."