public static async Task<Realm> GetRealm()
{
    var syncConfiguration = new SyncConfiguration("UnityTutorialPartition", RealmController.syncUser);
    return await Realm.GetInstanceAsync(syncConfiguration);
}
