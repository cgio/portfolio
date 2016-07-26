// Update new track rows added to the page after the fact

if (newTarget.target.className != null) {
	
	if (newTarget.target.className == 'bucket-items') {
		
		if (newTarget.nextSibling != null) {
			if (newTarget.nextSibling.className != null) {
				if ( $(newTarget.nextSibling).hasClass('bn1') == true ) {
					var bnPage = $('h1').text();
					if (bnPage == 'Hold Bin') {
						// Unlike the immediate behavior of automatically hiding the Beat Navigator row in the shopping cart page, execution reaches here after a delay when the Beat Navigator row has had time to display
						newTarget.nextSibling.parentNode.removeChild(newTarget.nextSibling);
					}
					if (bnPage == 'Shopping Cart') {
						// Clickinghide or remove
						$(newTarget.nextSibling).hide();
					}
				}
			}
		}

		// In the shopping cart, adding tracks and refreshing the page causes an issue because the new tracks in the cart are not given Beat Navigator rows unless we just do it as follows.

		if ( newTarget.target.innerHTML.indexOf('velocity-animating') == -1 && newTarget.target.innerHTML.indexOf('opacity: 0;') == -1) { // Only re-render Beat Navigator rows when the track is actually truly removed (the class is hidden, then actually removed by Beatport when a successful response is received by the cart api)
			var data = "bnEnable";
			var evt = document.createEvent("CustomEvent");
			evt.initCustomEvent("bnMessageFromMain", true, true, data);
			document.dispatchEvent(evt);
		}

	}

	if ( $(newTarget.target).hasClass('velocity-animating') == true ) {
		var bnPage = $('h1').text();
		if (bnPage == 'Hold Bin') {
			if (newTarget.target.nextSibling != null) {
				if ( $(newTarget.target.nextSibling).hasClass('bn1') == true) {
					newTarget.target.nextSibling.parentNode.removeChild(newTarget.target.nextSibling);
				}
			}
		}
	}

}
