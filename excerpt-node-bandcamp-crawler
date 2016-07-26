app.get('/f', function(req, res){ // Fan request
	var data = {url: new Buffer(req.query.q, 'base64').toString()};
	if (data.url) {
		if (isValidBandCampURL(data.url)) {
			var promiseResult = promiseGet(data)
				.then(function(result){
					if (result.statusCode == 200) {
						globalFanData.image_siteroot = extractImageSiteRoot(result.html);
						// We now have the page HTML for the album. Now we need to get data on each fan.
						globalFanData.fans = extractFans(result.html);
						if (globalFanData.fans.length > 0) {
							var promiseFanAlbums = [];
							globalFanData.fans.forEach(function(i) {
								var data = {url: 'http://bandcamp.com/' + i.username, username: i.username};
								var promiseResult = promiseGet(data);
								promiseFanAlbums.push(promiseResult);
							});
							Promise.all(promiseFanAlbums).then(function(results) {
								try {
									for (i = 0; i < results.length;  i++) {
										if (results[i].statusCode == 200) {
											var fanAlbums = extractFanData(results[i].html);
											for (f = 0; f < globalFanData.fans.length;  f++) {
												if (globalFanData.fans[f].username == results[i].username) {
													if (typeof fanAlbums !== 'undefined' && fanAlbums.length > 0) {
														globalFanData.fans[f]['albums'] = fanAlbums;
													}
													break;
												}
											}
										}
										else {
											// Bandcamp server did not respond properly, perhaps due to too many requests
											// HTTP response 504 (gateway timeout) was the most common error during testing
											// Should we implement a timeout?
										}
									}
									// Clean up the array by deleting any fans without albums
									for (f = 0; f < globalFanData.fans.length;  f++) {
										if (typeof globalFanData.fans[f].albums === 'undefined' || globalFanData.fans[f].albums.length == 0) {
											globalFanData.fans.splice(f, 1);
											f--;
										}
									}
									globalFanData.fans.filter(val => val); // Re-index array - note that the length does not change
									res.status(200);
									res.send(JSON.stringify(globalFanData));
								}
								catch(err) {
									res.status(500);
									res.send(JSON.stringify({Error: 'unexpected error (2)'}));
								}
							});
						}
						else {
							res.status(404);
							res.send(JSON.stringify({Error: 'this track or album is not popular yet'}));
						}
					}
					else {
						res.status(404);
						res.send(JSON.stringify({Error: 'no music found at this URL'}));
					}
				})
				.catch(function(err) {
					res.status(500);
					res.send(JSON.stringify({Error: 'unexpected error (1)'}));
				});
		}
		else {
			res.status(400);
			res.send(JSON.stringify({Error: 'invalid URL'}));
		}
	}
	else {
		res.status(400);
		res.send(JSON.stringify({Error: 'invalid URL'}));
	}
});
