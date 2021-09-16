// Observe collection notifications. Retain the token to keep observing.
listenerToken = realm.All<Stat>()
    .SubscribeForNotifications((sender, changes, error) =>
    {

        if (error != null)
        {
            // Show error message
            Debug.Log("an error occurred while listening for score changes :" + error);
            return;
        }

        if(changes != null)
        {
            setNewlyInsertedScores(changes.InsertedIndices);
        }
        // we only need to check for inserted because scores can't be modified or deleted after the run is complete
        
    });
