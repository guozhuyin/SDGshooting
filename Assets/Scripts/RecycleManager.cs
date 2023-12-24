using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class RecycleManager : MonoBehaviour
{
    public Button[] recycleButtons;
    public Sprite[] recycleSprites;
    public Image recycleImage;
    public TextMeshProUGUI recycleHead;
    public TextMeshProUGUI recycleContent;

    private int recyLen;

    void Start()
    {
        recyLen = recycleButtons.Length;
        for (int i = 0; i < recyLen; i++)
        {
            int index = i;
            recycleButtons[i].onClick.AddListener(() => RecyclePage(index));
        }
    }

    void RecyclePage(int index)
    {
        recycleHead.text = GetRecycleHeadText(index);
        recycleContent.text = GetRecycleContentText(index);
        recycleImage.sprite = recycleSprites[index];
    }

    string GetRecycleHeadText(int index)
    {
        string[] recycleHeadText = new string[]{
            "一般垃圾","塑膠","紙類","廚餘","玻璃","金屬","電子產品"
        };
        if (index < 0 || index >= recycleHeadText.Length)
        {
            return "Error";
        }
        return recycleHeadText[index];
    }

    string GetRecycleContentText(int index)
    {
        string[] recycleContentText = new string[]{
            "大多數送往掩埋場或焚化爐\n燃燒影響：垃圾焚燒可能會產生有毒氣體並且對空氣質量和人體健康有傷害，如二氧化硫攝入過量了可能造成呼吸困難、嘔吐、腹瀉等；一氧化碳會讓人頭痛、反胃，在濃度夠高時可能會造成昏迷或死亡。\n掩埋影響：垃圾掩埋場可能會因豪雨造成掩埋曾崩塌、土壤流失、廢氣溢散產生惡臭、引發火災等問題，且掩埋場會占用土地，導致土地資源浪費，同時，有機物質分解過程可能產生甲烷，吸入可能影響大腦和神經系統。",
            "石油節省：回收塑膠可以減少對原油的需求，有助於保護石油資源。\n減少塑膠垃圾：現今政府鼓勵大家少用免洗餐具，且同時對手搖飲業者設立了不能使用塑膠杯的規定，還透過省5元的方式來促進大家自行準備環保杯來購買飲料，甚至在更早之前就禁止商家免費提供購物用塑膠袋，以此減少塑膠類的垃圾量。\n\n不同類型的塑膠分解時間不同，一般塑膠袋大概10-20年，而有些塑膠可能需要數百年才能完全分解。",
            "減少樹木需求：據統計，每噸回收紙張可以節省17棵成年樹木的伐木，減少伐木活動對森林生態的影響。\n能源節約：製造再生紙相較於新紙可以節省大量的水和能源。\n\n紙張在掩埋場中分解時間大約為2-6週。",
            "有機堆肥：堆肥處理可以生產有機肥料，提高土壤肥力，減少對化肥的需求。\n減少甲烷排放：堆肥過程相當於填埋場，只是產生的甲烷量更少，有助於減緩溫室氣體的排放。\n\n在堆肥處理中，有機物可以在數周到數月內分解成堆肥。",
            "回收的玻璃可以在此融化製成新的玻璃製品，相較於初次製造使用原材料更節省能源，降低二氧化碳排放。\n\n1-200年， 玻璃的分解時間不確定，但可以無限次循環再生。",
            "節省礦產資源：回收金屬罐可以減少對礦產資源的需求，並同時減少開採對自然環境的損害。\n減少能源使用：經過處理後的鐵鋁罐可以製成新的金屬製品，相較於初次提煉更能節省能源消耗，同時降低生產新金屬的碳足跡。\n\n金屬罐通常不會分解，但回收再利用可以永久循環。",
            "資源再利用；適當的處理3C用品的回收零件，可以有效利用有價值的金屬和塑膠資源。\n減少電子垃圾影響：避免3C用品進入填埋場可以減少釋放出的有毒物質對環境的汙染。\n\n電子產品的一些零件可能需要數十年或更長時間才能自然分解，而其他部分可能永遠不會分解，此外電池須超過200年的時間，並會有毒的學物質。"
        };
        if (index < 0 || index >= recycleContentText.Length)
        {
            return "Error";
        }
        return recycleContentText[index];
    }
}