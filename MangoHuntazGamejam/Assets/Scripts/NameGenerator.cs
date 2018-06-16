using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour {
    public string[] extensions = { "HD Premaster", "Collector's", "Ultimate Edition", "Uncut", "Director's Cut"};
    private string originaltitle = "Ultimate Frightmare";
    public int ONE_IN_X_CHANCE_TO_ADD_POSTFIX=10;
    public string NameGen()
    {
        string output = originaltitle;
        foreach (string add in extensions) {
            if (Random.Range(1, ONE_IN_X_CHANCE_TO_ADD_POSTFIX) == ONE_IN_X_CHANCE_TO_ADD_POSTFIX)
                output +=" "+ add;
                    }
        return output;
    }
}
