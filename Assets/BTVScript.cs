using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BTVScript : MonoBehaviour {

    public AudioSource BGM;
    public KMAudio Audio;
    public KMBombModule module;
    public KMBossModule boss;
    public KMBombInfo info;
    public KMSelectable modselect;
    public Transform sparkholder;
    public ParticleSystem[] modsmash;
    public RenderTexture pixelate;
    public GameObject pixelimage;
    public GameObject matstore;
    public List<KMSelectable> buttons;
    public Transform[] bts;
    public GameObject[] screens;
    public GameObject[] smallscreens;
    public GameObject[] screenglass;
    public Light backlight;
    public Transform[] glitchbm;
    public Transform hand;
    public Renderer crack;
    public Material crackmat;
    public Renderer[] bombtimer;
    public Material[] bombmats;
    public Renderer[] lift;
    public Material[] liftmats;
    public Transform[] disppos;
    public TextMesh[] disptexts;
    public AudioClip[] bgms;
    public Renderer[] arrowrends;
    public Material[] arrowmats;
    public Transform[] arrowt;
    public GameObject[] avoelements;
    public Renderer[] avorends;
    public Material[] avomats;
    public TextMesh avodisp;
    public GameObject[] stackelements;
    public Renderer[] stackrends;
    public Material[] stackmats;
    public Transform[] stackmag;
    public TextMesh stackdisp;
    public GameObject[] assaelements;
    public Renderer[] assarends;
    public Material[] assamats;
    public GameObject[] duelelements;
    public Renderer[] duelrends;
    public Material[] duelmats;
    public Transform ligret;
    public Renderer[] ligrends;
    public Material[] ligmats;
    public GameObject[] couelements;
    public Renderer[] courends;
    public Material[] coumats;
    public TextMesh[] coudisps;
    public Transform[] couhost;
    public GameObject[] mashelements;
    public Renderer[] mashrends;
    public Material[] mashmats;
    public TextMesh mashtext;
    public ParticleSystem mashdust;
    public GameObject[] snakelements;
    public Renderer[] snakrends;
    public Material[] snakmats;
    public GameObject[] silelements;
    public Renderer[] silrends;
    public Material[] silmats;
    public TextMesh[] sildisps;
    public Transform[] whacelements;
    public Renderer[] whacrends;
    public Material[] whacmats;
    public TextMesh[] whacdisps;
    public GameObject[] bosselements;
    public Renderer[] bossrends;
    public Material[] bossmats;
    public TextMesh bossquote;
    public ParticleSystem bossfire;

    private string[] exempt;
    private readonly string[] anticheese = new string[] { "The Simpleton", "Toolmods", "Dossier Modifier", "Free Password", "Large Free Password", "Press The Shape", "Color Numbers", "Button Order", "Just Numbers", "Gatekeeper", "Digital Root", "Combination Lock", "Color One Two", "Logging", "Base-1", "Antistress", "egg" };
    private List<KeyCode> keys = new List<KeyCode> { KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.Space, KeyCode.DownArrow, KeyCode.RightArrow };
    private bool focus;
    private readonly string[] prompts = new string[] { "Follow!", "Collect!", "Arrange!", "Snipe!", "Defeat!", "Match!", "Count!", "Mash!", "Eat!", "Identify!", "Whack!" };
    private int stage;
    private int phase;
    private int limit;
    private int game;
    private float speed = 1;
    private int lives = 4;
    private bool? pass;
    private bool irredeemablefailure;
    private int arrowcount;
    private int arrowinstr;
    private int arrowans;
    private int[] avopos = new int[2];
    private int[] avospawn = new int[2];
    private int avonum;
    private int[] avoshoot = new int[5];
    private List<int>[] stacks = new List<int>[4] { new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { } };
    private int[] stackpos = new int[2] { 0, -1 };
    private bool stackmoving;
    private bool[] assamoving = new bool[2];
    private float[] assacross = new float[2];
    private bool[] assareveal = new bool[7];
    private int dueldir;
    private bool[] duelbool = new bool[2];
    private bool[,] ligio = new bool[2, 9];
    private int ligpos = 4;
    private int[] counts = new int[2] { 0, 15 };
    private bool countphase;
    private int mashcount;
    private int mashpunch;
    private float mashcooldown = 0;
    private List<int> snake;
    private List<int> snakdice = new List<int> { };
    private int snaklength;
    private int snakdir;
    private bool snakturn;
    private readonly string[] silnames = new string[64] { "ANCHOR", "ANKH", "APPLE", "BALLOON", "BANANA", "BARRELL", "BASEBALL\nBAT", "BRIEFCASE", "BED", "BOLT", "BUTTON", "CAULDRON", "CAR", "DETONATOR", "CAPSULE", "DUMBBELL", "COG", "DIE", "KING", "DOUGHNUT", "LADDER", "DREIDEL", "PICTURE\nFRAME", "CONICAL\nFLASK", "FRISBEE", "CAGE", "HOUSE", "GOBLET", "ICE\nCREAM", "HOURGLASS", "JACK", "MILK\nCARTON", "LIGHT\nBULB",
        "SEA\nMINE", "MAGNET", "METHANE\nMOLECULE", "COFFEE\nMUG", "MUSHROOM", "STACK\nOF\nNUTS", "PAINT\nBRUSH", "PENCIL", "PINE\nTREE", "PICKAXE", "SHIP'S\nHELM", "SLICE\nOF\nPIE", "PLANT\nPOT", "DRAWING\nPIN", "PYRAMID", "TRAIN", "ROCKET", "RUBIK'S\nCUBE", "PAPER\nBOAT", "PHONE", "SATURN", "SOFA", "ERASER", "SPOON", "TABLE", "TEAPOT", "TEXTBOOK", "SALT\nSHAKER", "TOP\nHAT", "TOWER", "WATERING\nCAN"};
    private bool[] silrot = new bool[2];
    private List<int> silopts = new List<int> { };
    private int silselect = 4;
    private bool silphase;
    private float[] whacz = new float[6] { -0.68f, 0.8f, 0.17f, 2.18f, 1.38f, 3.8f};
    private Vector3[] whacpositions = new Vector3[] { new Vector3(1.706f, 0.0004f, -1.73f), new Vector3(0.204f, 0.0004f, -1.73f), new Vector3(-1.315f, 0.0004f, -1.73f), new Vector3(2.17f, 0.0007f, -1.31f), new Vector3(0.246f, 0.0007f, -1.31f), new Vector3(-1.723f, 0.0007f, -1.31f), new Vector3(2.79f, 0.001f, -0.58f), new Vector3(0.32f, 0.001f, -0.58f), new Vector3(-2.19f, 0.001f, -0.58f) };
    private readonly string[] whacnames = new string[6] { "COWBOYS!", "PIRATES!", "VIKINGS!", "JESTERS!", "SHOGUNS!", "CYBORGS!" };
    private int[] whaccostume = new int[9];
    private int whaclimit;
    private int[] whacpos = new int[2];
    private int whacforbid = 6;
    private int[] whacscore = new int[2];
    private bool whacswing;
    private bool[] bossfaceleft = new bool[2] { false, true};
    private bool[] bossmovement = new bool[2];
    private bool[] bossshoot = new bool[2];
    private bool[] bossdamage = new bool[2];
    private int bossstability = 400;
    private int bossjumps = 2;
    private int bossattack;
    private int bossanim;
    private int[] bossthrow = new int[3];
    private Camera mainCamera;
    private Vector3 campos;

    private bool moduleSolved;

    private void Start()
    {
        mainCamera = Camera.main;
        campos = mainCamera.transform.localPosition;
        exempt = boss.GetIgnoredModules("BadTV", new string[] { "BadTV" });
        module.OnActivate = Activate;
        modselect.OnFocus += delegate () { focus = true; };
        modselect.OnDefocus += delegate () { focus = false; };
    }

    private void Activate()
    {
        matstore.SetActive(false);
        backlight.intensity /= info.GetSolvableModuleNames().Count(x => x == "BadTV");
        limit = info.GetSolvableModuleNames().Count(x => !exempt.Contains(x));
        if (limit < 1)
        {
            phase = 1;
            limit = 11;
            StartCoroutine("Begin");
        }
        else
        {
            int ex = 0;
            ex += limit;
            ex += (info.GetSolvableModuleNames().Count() - limit) * 2;
            ex += (info.GetModuleNames().Count() - info.GetSolvableModuleNames().Count()) * 3;
            lives += ex / 25;
            limit *= 3;
            limit /= 2;
            limit = Mathf.Max(12, limit + 1);
            StartCoroutine("Wait");
        }
        limit += info.GetSolvableModuleNames().Count(x => anticheese.Contains(x));
        disptexts[3].text = (lives < 10 ? "0" : "") + lives.ToString();
        foreach(KMSelectable button in buttons)
        {
            int b = buttons.IndexOf(button);
            button.OnInteract = delegate () { 
                StartCoroutine("ButtonDown", b);
                return false;
            };
            button.OnInteractEnded = delegate () { 
                StartCoroutine("ButtonUp", b);
            };
        }
        info.OnBombExploded += delegate ()
        {
            OnDestroy();
        };   
    }

    private void OnDestroy()
    {
        BGM.Stop();
    }

    private void Update()
    {
        if (focus || Application.isEditor)
        {
            foreach (KeyCode key in keys)
            {
                int k = keys.IndexOf(key);
                if (Input.GetKeyDown(key))
                    StartCoroutine("ButtonDown", k);
                if (Input.GetKeyUp(key))
                    StartCoroutine("ButtonUp", k);
            }
        }
    }

    private IEnumerator ButtonDown(int b)
    {
        if (!moduleSolved)
        {
            Vector3 xz = bts[b].localPosition;
            bts[b].localPosition = new Vector3(xz.x, 0.0095f, xz.z);
            if (!irredeemablefailure && phase > 0)
            {
                switch (game)
                {
                    case 1:
                        if(arrowcount < 15)
                        {
                            arrowrends[2].enabled = false;
                            if (arrowans == b)
                            {
                                arrowcount++;
                                if(arrowcount > 14)
                                {
                                    pass = true;
                                    Audio.PlaySoundAtTransform("PassFanfare3", transform);
                                    arrowrends[0].enabled = false;
                                    arrowrends[1].enabled = false;
                                    arrowrends[3].enabled = true;
                                    arrowrends[3].material = arrowmats[Random.Range(10, 12)];
                                    float e = 1.24f;
                                    while(e > 1)
                                    {
                                        e -= Time.deltaTime;
                                        arrowt[2].localScale = new Vector3(e, 1, e);
                                        yield return null;
                                    }
                                }
                                else
                                {
                                    Audio.PlaySoundAtTransform("SFXBlipGood", transform);
                                    yield return new WaitForSeconds(0.1f);
                                    arrowrends[2].enabled = true;
                                    arrowinstr = Random.Range(0, 3);
                                    if (arrowinstr == 1)
                                        arrowans = 2;
                                    else
                                    {
                                        int a = Random.Range(0, 4);
                                        arrowans = new int[4] { 1, 4, 3, 0 }[a];
                                        arrowt[0].localEulerAngles = new Vector3(0, 90 * a, 0);
                                    }
                                    arrowrends[2].material = arrowmats[(2 * arrowinstr) + 4 + arrowrends[2].material.name.First() - '1'];
                                }
                            }
                            else
                            {
                                irredeemablefailure = true;
                                Audio.PlaySoundAtTransform("FailSink", transform);
                                StopCoroutine("Game1");
                                arrowrends[4].enabled = true;
                                float e = 1;
                                float spin = Random.Range(360, 720);
                                while(e > 0)
                                {
                                    e -= Time.deltaTime;
                                    arrowt[1].localScale = new Vector3(e, 1, e);
                                    arrowt[1].localEulerAngles = new Vector3(0, e * spin, 0);
                                    float s = (e / 5) + 1;
                                    arrowt[3].localScale = new Vector3(s, 1, s);
                                    yield return null;
                                }
                            }
                        }
                        break;
                    case 2:
                        if (avonum < 6)
                        {
                            switch (b)
                            {
                                case 0:
                                    if (avopos[1] > 0)
                                        avopos[1]--;
                                    break;
                                case 1:
                                    if (avopos[0] > 0)
                                        avopos[0]--;
                                    break;
                                case 3:
                                    if (avopos[0] < 4)
                                        avopos[0]++;
                                    break;
                                case 4:
                                    if (avopos[1] < 4)
                                        avopos[1]++;
                                    break;
                            }
                            Audio.PlaySoundAtTransform("SFXBlip", transform);
                            avoelements[2].transform.localPosition = new Vector3((avopos[1] * 0.944f) - 1.908f, 0.0012f, 2.68f - (avopos[0] * 1.33f));
                            if (avopos[0] == avospawn[0] && avopos[1] == avospawn[1])
                            {
                                avonum++;
                                if (avonum < 6)
                                {
                                    Audio.PlaySoundAtTransform("SFXCorrect", transform);
                                    int a = Enumerable.Range(0, 25).Where(x => Mathf.Abs(x / 5 - avopos[0]) + Mathf.Abs(x % 5 - avopos[1]) > 3).PickRandom();
                                    avospawn[0] = a / 5;
                                    avospawn[1] = a % 5;
                                    avoelements[3].transform.localPosition = new Vector3((avospawn[1] * 0.944f) - 1.908f, 0.001f, 2.68f - (avospawn[0] * 1.33f));
                                    avorends[2].material = avomats[Random.Range(7, 14)];
                                }
                                else
                                {
                                    pass = true;
                                    Audio.PlaySoundAtTransform("PassRegister", transform);
                                    avoelements[0].SetActive(false);
                                    avoelements[1].SetActive(true);
                                    avorends[0].enabled = false;
                                    for (int i = 43; i < 83; i++)
                                        avorends[i].enabled = false;
                                    int[] a = Enumerable.Range(0, 40).Select(x => x / 10).ToArray().Shuffle();
                                    int[] c = new int[4];
                                    avodisp.text = "GOLD!";
                                    for(int i = 0; i < 40; i++)
                                    {
                                        int x = a[i];
                                        if (c[x] > 0)
                                            avorends[42 + (x * 10) + c[x]].material = avomats[25];
                                        c[x]++;
                                        avorends[42 + (x * 10) + c[x]].material = avomats[24];
                                        avorends[42 + (x * 10) + c[x]].enabled = true;
                                        if (i % 10 == 9)
                                        {
                                            avodisp.text = "G";
                                            for (int j = 0; j < (i / 10) + 2; j++)
                                                avodisp.text += "O";
                                            avodisp.text += "LD!";
                                        }
                                        yield return new WaitForSeconds(0.03f);
                                    }
                                }
                            }

                        }
                        break;
                    case 3:
                        if (pass == false && !stackmoving)
                        {
                            float e = 0;
                            float[] s = new float[2];
                            switch (b)
                            {
                                case 0:
                                    if (stackpos[0] < 1)
                                    {
                                        Audio.PlaySoundAtTransform("SFXInvalid", transform);
                                        break;
                                    }
                                    Audio.PlaySoundAtTransform("SFXBuzz", transform);
                                    stackmoving = true;
                                    s = new float[2] { stackmag[0].localPosition.x, new float[3] { 3.68f, 1.2266f, -1.2266f }[stackpos[0] - 1] };
                                    while (e < 0.2f)
                                    {
                                        e += Time.deltaTime * speed;
                                        stackmag[0].localPosition = new Vector3(Mathf.Lerp(s[0], s[1], e * 5), 0.0008f, -3.9f);
                                        yield return null;
                                    }
                                    stackpos[0]--;
                                    stackmoving = false;
                                    stackmag[0].localPosition = new Vector3(s[1], 0.0008f, -3.9f);
                                    break;
                                case 4:
                                    if (stackpos[0] > 2)
                                    {
                                        Audio.PlaySoundAtTransform("SFXInvalid", transform);
                                        break;
                                    }
                                    Audio.PlaySoundAtTransform("SFXBuzz", transform);
                                    stackmoving = true;
                                    s = new float[2] { stackmag[0].localPosition.x, new float[3] { 1.2266f, -1.2266f, -3.68f }[stackpos[0]] };
                                    while (e < 0.2f)
                                    {
                                        e += Time.deltaTime * speed;
                                        stackmag[0].localPosition = new Vector3(Mathf.Lerp(s[0], s[1], e * 5), 0.0008f, -3.9f);
                                        yield return null;
                                    }
                                    stackpos[0]++;
                                    stackmoving = false;
                                    stackmag[0].localPosition = new Vector3(s[1], 0.0008f, -3.9f);
                                    break;
                                case 3:
                                    e = 0.39f;
                                    float[] heights = new float[3] { 5.45f, 3.93f, 2.45f};
                                    float d = 0;
                                    if (stackpos[1] < 0)
                                    {
                                        if(stacks[stackpos[0]].Count() < 1)
                                        {
                                            Audio.PlaySoundAtTransform("SFXInvalid", transform);
                                            yield break;
                                        }
                                        stackmoving = true;
                                        d = heights[stacks[stackpos[0]].Count() - 1];
                                        while (e < d)
                                        {
                                            e += Time.deltaTime * 20 * speed;
                                            stackmag[1].localPosition = new Vector3(0, -0.0002f, (e - 0.23f) / 2);
                                            stackmag[1].localScale = new Vector3(0.03f, 1, (e - 0.39f) / 10);
                                            stackmag[2].localPosition = new Vector3(0, 0.0003f, e);
                                            yield return null;
                                        }
                                        stackpos[1] = stacks[stackpos[0]].Last();
                                        stackrends[(3 * stackpos[0]) + stacks[stackpos[0]].Count()].enabled = false;
                                        stacks[stackpos[0]].RemoveAt(stacks[stackpos[0]].Count() - 1);
                                        stackrends[0].material = stackmats[stackpos[1]];
                                        stackrends[0].enabled = true;
                                        while (e > 0.36f)
                                        {
                                            e -= Time.deltaTime * 30 * speed;
                                            stackmag[1].localPosition = new Vector3(0, -0.0002f, (e - 0.23f) / 2);
                                            stackmag[1].localScale = new Vector3(0.03f, 1, (e - 0.39f) / 10);
                                            stackmag[2].localPosition = new Vector3(0, 0.0003f, e);
                                            yield return null;
                                        }
                                        stackmag[1].localPosition = new Vector3(0, -0.0002f, 0.16f);
                                        stackmag[1].localScale = new Vector3(0.03f, 1, 0);
                                        stackmag[2].localPosition = new Vector3(0, 0.0003f, 0.39f);
                                        stackmoving = false;
                                    }
                                    else
                                    {
                                        if (stacks[stackpos[0]].Count() > 2)
                                        {
                                            Audio.PlaySoundAtTransform("SFXInvalid", transform);
                                            yield break;
                                        }
                                        stackmoving = true;
                                        d = heights[stacks[stackpos[0]].Count()];
                                        while (e < d)
                                        {
                                            e += Time.deltaTime * 30 * speed;
                                            stackmag[1].localPosition = new Vector3(0, -0.0002f, (e - 0.23f) / 2);
                                            stackmag[1].localScale = new Vector3(0.03f, 1, (e - 0.39f) / 10);
                                            stackmag[2].localPosition = new Vector3(0, 0.0003f, e);
                                            yield return null;
                                        }
                                        stacks[stackpos[0]].Add(stackpos[1]);
                                        stackrends[(3 * stackpos[0]) + stacks[stackpos[0]].Count()].material = stackmats[stackpos[1]];
                                        stackrends[(3 * stackpos[0]) + stacks[stackpos[0]].Count()].enabled = true;
                                        stackpos[1] = -1;
                                        stackrends[0].enabled = false;
                                        Audio.PlaySoundAtTransform("SFXCrate", transform);
                                        if(stackpos[1] < 0)
                                        {
                                            for (int i = 0; i < 4; i++)
                                                Debug.LogFormat(string.Join(", ", stacks[i].Select(x => x.ToString()).ToArray()));
                                        }
                                        if(stacks.All(x => x.Distinct().Count() < 2 && x.Count() % 3 == 0))
                                        {
                                            pass = true;
                                            Audio.PlaySoundAtTransform("PassFanfare2", transform);
                                            StartCoroutine("StackPass");
                                        }
                                        while (e > 0.36f)
                                        {
                                            e -= Time.deltaTime * 20 * speed;
                                            stackmag[1].localPosition = new Vector3(0, -0.0002f, (e - 0.23f) / 2);
                                            stackmag[1].localScale = new Vector3(0.03f, 1, (e - 0.39f) / 10);
                                            stackmag[2].localPosition = new Vector3(0, 0.0003f, e);
                                            yield return null;
                                        }
                                        stackmag[1].localPosition = new Vector3(0, -0.0002f, 0.16f);
                                        stackmag[1].localScale = new Vector3(0.03f, 1, 0);
                                        stackmag[2].localPosition = new Vector3(0, 0.0003f, 0.39f);
                                        stackmoving = false;
                                    }
                                    break;
                            }
                        }
                        break;
                    case 4:
                        if (b == 2)
                        {
                            if(pass == false && !irredeemablefailure)
                            {
                                Audio.PlaySoundAtTransform("SFXSnipe", transform);
                                int n = -1;
                                float y = -4f;
                                for(int i = 0; i < 7; i++)
                                {
                                    if(assareveal[i] && assaelements[i].transform.localPosition.z > y)
                                    {
                                        n = i;
                                        y = assaelements[i].transform.localPosition.z;
                                    }
                                }
                                pass = n == 0;
                                irredeemablefailure = n != 0;
                                yield return null;
                                for(int i = 0; i < 7; i++)
                                {
                                    if (i == n)
                                    {
                                        Audio.PlaySoundAtTransform("SFXGrunt", transform);
                                        for (int j = 1; j < 6; j++)
                                            assarends[(j * 7) + i].enabled ^= true;
                                    }
                                    else
                                    {
                                        assarends[i].enabled = true;
                                        for (int j = 1; j < 5; j++)
                                            assarends[(j * 7) + i].enabled = false;
                                        if (i > 0)
                                            StartCoroutine(AssaSkidaddle(i, assacross[0]));
                                    }
                                }
                                if (irredeemablefailure)
                                {
                                    Audio.PlaySoundAtTransform("FailMonster", transform);
                                    assarends[42].enabled = true;
                                    bool g = false;
                                    while(game > 0)
                                    {
                                        g ^= true;
                                        assarends[42].material = assamats[g ? 21 : 22];
                                        yield return new WaitForSeconds(0.5f);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!assamoving[b % 2] && pass == false && !irredeemablefailure)
                            {
                                assamoving[b % 2] = true;
                                switch (b)
                                {
                                    case 0:
                                        while(assamoving[0] && assacross[0] < 4.23f)
                                        {
                                            assacross[0] += Time.deltaTime * 3.2f * speed;
                                            assaelements[7].transform.localPosition = new Vector3(assacross[0], 0.0075f, assacross[1]);
                                            yield return null;
                                        }
                                        break;
                                    case 1:
                                        while (assamoving[1] && assacross[1] > -2.27f)
                                        {
                                            assacross[1] -= Time.deltaTime * 3.2f * speed;
                                            assaelements[7].transform.localPosition = new Vector3(assacross[0], 0.0075f, assacross[1]);
                                            yield return null;
                                        }
                                        break;
                                    case 3:
                                        while (assamoving[1] && assacross[1] < 2.68f)
                                        {
                                            assacross[1] += Time.deltaTime * 3.2f * speed;
                                            assaelements[7].transform.localPosition = new Vector3(assacross[0], 0.0075f, assacross[1]);
                                            yield return null;
                                        }
                                        break;
                                    case 4:
                                        while (assamoving[0] && assacross[0] > -4.23f)
                                        {
                                            assacross[0] -= Time.deltaTime * 3.2f * speed;
                                            assaelements[7].transform.localPosition = new Vector3(assacross[0], 0.0075f, assacross[1]);
                                            yield return null;
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case 5:
                        if (!duelbool[1])
                        {
                            pass = duelbool[0] && b == dueldir;
                            duelbool[1] = true;
                        }
                        break;
                    case 6:
                        if (pass != true)
                        {
                            switch (b)
                            {
                                case 0:
                                    if (ligpos % 3 > 0)
                                    {
                                        Audio.PlaySoundAtTransform("SFXBlip", transform);
                                        ligpos--;
                                    }
                                    break;
                                case 1:
                                    if (ligpos / 3 > 0)
                                    {
                                        Audio.PlaySoundAtTransform("SFXBlip", transform);
                                        ligpos -= 3;
                                    }
                                    break;
                                case 3:
                                    if (ligpos / 3 < 2)
                                    {
                                        Audio.PlaySoundAtTransform("SFXBlip", transform);
                                        ligpos += 3;
                                    }
                                    break;
                                case 4:
                                    if (ligpos % 3 < 2)
                                    {
                                        Audio.PlaySoundAtTransform("SFXBlip", transform);
                                        ligpos++;
                                    }
                                    break;
                                default:
                                    Audio.PlaySoundAtTransform("SFXFlick", transform);
                                    foreach(int l in Ligtoggle(ligpos))
                                    {
                                        ligio[1, l] ^= true;
                                        ligrends[l + 1].enabled ^= true;
                                    }
                                    if(Enumerable.Range(0, 9).Select(x => ligio[0, x] == ligio[1, x]).All(x => x))
                                    {
                                        pass = true;
                                        Audio.PlaySoundAtTransform("PassFanfare4", transform);
                                        for(int i = 0; i < 4; i++)
                                        {
                                            for (int j = 1; j < 19; j++)
                                                ligrends[j].enabled = i % 2 == 0;
                                            yield return new WaitForSeconds(0.08f);
                                        }
                                        yield return new WaitForSeconds(1.4f / speed);
                                        ligrends[0].enabled = false;
                                        if (game > 0)
                                        {
                                            Audio.PlaySoundAtTransform("SFXDoon", transform);
                                            ligrends[19].material = ligmats[Random.Range(1, 4)];
                                        }
                                    }
                                    break;
                            }
                            ligret.localPosition = new Vector3(new float[3] { 12.53f, 0.2f, -12.13f}[ligpos % 3], 0.003f, new float[3] { 0.44f, 9.9f, 18.52f}[ligpos / 3]);
                        }
                        break;
                    case 7:
                        if(b % 2 == 1 && !countphase)
                        {
                            courends[42].material = coumats[16];
                            couelements[40].transform.localPosition = new Vector3(0, 0.0002f, 0.17f);
                            Audio.PlaySoundAtTransform("SFXSlap", transform);
                            if (b == 1 && counts[1] < 30)
                                counts[1]++;
                            else if (b == 3 && counts[1] > 15)
                                counts[1]--;
                            coudisps[2].text = counts[1].ToString();
                            yield return new WaitForSeconds(0.1f);
                            courends[42].material = coumats[15];
                            couelements[40].transform.localPosition = new Vector3(0, 0.0002f, 0);
                        }
                        break;
                    case 8:
                        if (mashcount > 0 && b != 2)
                        {
                            mashcount--;
                            mashtext.text = mashcount.ToString();
                            mashelements[1].transform.localPosition = new Vector3(Random.Range(-0.37f, 0.37f), 0, Random.Range(-0.14f, 0.14f) );
                            int c = Random.Range(0, 4);
                            mashelements[2].transform.localScale = new Vector3(1.05f * ((2 * (c / 2)) - 1), 1, 1.05f * ((2 * (c % 2)) - 1));
                            mashrends[0].material = mashmats[Random.Range(0, 8)];
                            if (Random.Range(0, 10) < 1)
                            {
                                Audio.PlaySoundAtTransform("SFXPunchBig", transform);
                                mashrends[3].enabled = true;
                                yield return null;
                                mashrends[3].enabled = false;
                            }
                            else
                                Audio.PlaySoundAtTransform("SFXPunch", transform);
                            if (mashcount > 0)
                            {
                                if (mashcooldown <= 0)
                                {
                                    mashcooldown = 0.15f;
                                    while (mashcooldown > 0 && mashcount > 0)
                                    {
                                        float s = Mathf.Lerp(1, 0.75f, mashcooldown / 0.15f);
                                        mashelements[4].transform.localScale = new Vector3(s, 1, s);
                                        mashcooldown -= Time.deltaTime * speed;
                                        yield return null;
                                    }
                                    if (mashcount > 0)
                                    {
                                        mashelements[2].transform.localScale = new Vector3(1, 1, 1);
                                        mashrends[0].material = mashmats[8];
                                    }
                                }
                                else
                                    mashcooldown = 0.15f;
                            }
                            else
                            {
                                mashtext.text = "FINISH";
                                mashrends[2].material = mashmats[14];
                                mashcooldown = 0.15f;
                                while (mashcooldown > 0)
                                {
                                    float s = Mathf.Lerp(1, 1.5f, mashcooldown / 0.15f);
                                    mashelements[4].transform.localScale = new Vector3(s, 1, s);
                                    mashcooldown -= (Time.deltaTime * speed) / 2;
                                    yield return null;
                                }
                                mashelements[2].transform.localScale = new Vector3(1, 1, 1);
                                mashrends[0].material = mashmats[8];
                            }
                        }
                        else if(pass == false && mashcount <= 0 && b == 2)
                        {
                            pass = true;
                            mashelements[0].SetActive(false);
                            mashelements[5].SetActive(true);
                            mashelements[6].SetActive(false);
                            Audio.PlaySoundAtTransform("SFXWindup", transform);
                            for(int i = 0; i < 8; i++)
                            {
                                mashrends[4].material = mashmats[i + 15];
                                yield return new WaitForSeconds(0.04f);
                            }
                            mashrends[5].material = mashmats[30];
                            mashelements[6].SetActive(true);
                            yield return new WaitForSeconds(0.46f);
                            if (game < 1)
                                yield break;
                            mashrends[4].material = mashmats[23];
                            Audio.PlaySoundAtTransform("SFXExplosion", transform);
                            mashdust.Emit(30);
                            yield return new WaitForSeconds(0.04f);
                            mashrends[4].material = mashmats[24];
                            mashrends[5].material = mashmats[Random.Range(27, 30)];
                            yield return new WaitForSeconds(0.46f);
                            int j = 0;
                            if (game < 1)
                                yield break;
                            Audio.PlaySoundAtTransform("PassLaugh", transform);
                            while(game > 0)
                            {
                                j ^= 1;
                                mashrends[4].material = mashmats[j + 25];
                                yield return new WaitForSeconds(0.15f);
                            }
                        }
                        break;
                    case 9:
                        if(!snakturn && snakdir != 4 - b)
                        {
                            snakturn = true;
                            snakdir = b;
                            Audio.PlaySoundAtTransform("SFXBlip", transform);
                        }                       
                        break;
                    case 10:
                        if (pass == false)
                        {
                            if (!silphase)
                            {
                                if (!silrot[0] && (b == 0 || b == 4))
                                {
                                    silrot[0] = true;
                                    StartCoroutine(Silrotate((b / 2) - 1, 0));
                                }
                                if (!silrot[1] && (b == 1 || b == 3))
                                {
                                    silrot[1] = true;
                                    StartCoroutine(Silrotate(0, b - 2));
                                }
                            }
                            else
                            {
                                StopCoroutine("Silswitch");
                                switch (b)
                                {
                                    case 0:
                                        if(silselect % 3 > 0)
                                        {
                                            sildisps[silselect].transform.localEulerAngles = new Vector3(90, 180, 0);
                                            sildisps[silselect].fontSize = 200;
                                            sildisps[silselect].color = new Color(0, 200f / 255, 0);
                                            silselect--;
                                            StartCoroutine("Silswitch", silselect);
                                        }
                                        break;
                                    case 1:
                                        if (silselect > 2)
                                        {
                                            sildisps[silselect].transform.localEulerAngles = new Vector3(90, 180, 0);
                                            sildisps[silselect].fontSize = 200;
                                            sildisps[silselect].color = new Color(0, 200f / 255, 0);
                                            silselect -= 3;
                                            StartCoroutine("Silswitch", silselect);
                                        }
                                        break;
                                    case 2:
                                        sildisps[9].text = silnames[silopts[9]].Replace('\n', ' ');
                                        silelements[3].SetActive(false);
                                        silelements[4].SetActive(true);
                                        silrends[silopts[9]].enabled = true;
                                        silelements[1].transform.localScale = new Vector3(6.5f, 0.02f, 6.5f);
                                        if(silopts[silselect] == silopts[9])
                                        {                                            
                                            pass = true;
                                            Audio.PlaySoundAtTransform("PassApplause", transform);
                                            silrends[65].material = silmats[4];
                                        }
                                        else
                                        {                                           
                                            irredeemablefailure = true;
                                            Audio.PlaySoundAtTransform("FailWrongAnswer", transform);
                                            silrends[65].material = silmats[5];
                                        }
                                        if(pass == true)
                                            while(game > 0)
                                            {
                                                silelements[5].transform.localScale = new Vector3(-silelements[5].transform.localScale.x, 1, 1);
                                                yield return new WaitForSeconds(0.15f / speed);
                                                silelements[5].transform.localScale = new Vector3(-silelements[5].transform.localScale.x, 1, 1);
                                                yield return new WaitForSeconds(0.15f / speed);
                                                silelements[4].transform.localScale = new Vector3(-silelements[4].transform.localScale.x, 1, 1);
                                                yield return new WaitForSeconds(0.15f / speed);
                                            }
                                        break;
                                    case 3:
                                        if (silselect < 6)
                                        {
                                            sildisps[silselect].transform.localEulerAngles = new Vector3(90, 180, 0);
                                            sildisps[silselect].fontSize = 200;
                                            sildisps[silselect].color = new Color(0, 200f / 255, 0);
                                            silselect += 3;
                                            StartCoroutine("Silswitch", silselect);
                                        }
                                        break;
                                    case 4:
                                        if (silselect % 3 < 2)
                                        {
                                            sildisps[silselect].transform.localEulerAngles = new Vector3(90, 180, 0);
                                            sildisps[silselect].fontSize = 200;
                                            sildisps[silselect].color = new Color(0, 200f / 255, 0);
                                            silselect++;
                                            StartCoroutine("Silswitch", silselect);
                                        }
                                        break;
                                }
                                Debug.Log(silnames[silopts[silselect]].Replace('\n', ' '));
                            }
                        }
                        break;
                    case 11:
                        switch (b)
                        {
                            case 0:
                                if (whacpos[0] % 3 > 0)
                                    whacpos[0]--;
                                break;
                            case 1:
                                if (whacpos[0] / 3 > 0)
                                    whacpos[0] -= 3;
                                break;
                            case 2:
                                if (!whacswing)
                                {
                                    whacswing = true;
                                    whacrends[9].material = whacmats[8];
                                    if (whaccostume[whacpos[1]] >= 0)
                                    {
                                        Audio.PlaySoundAtTransform("SFXWhack", transform);
                                        if(whaccostume[whacpos[1]] == whacforbid)
                                        {
                                            irredeemablefailure = true;
                                            whacrends[10].enabled = true;
                                            Audio.PlaySoundAtTransform("FailAlarm", transform);
                                            for (int i = 0; i < 4; i++)
                                                whacdisps[i].text = "";
                                            yield return new WaitForSeconds(2.2f);
                                            screens[11].SetActive(false);
                                            BGM.Stop();
                                            Audio.PlaySoundAtTransform("FailBonk", transform);
                                        }
                                        else
                                        {
                                            whacscore[0]++;
                                            whacdisps[0].text = (whacscore[0] > 9 ? "" : "0") +whacscore[0].ToString();
                                            if(whacscore[0] > whacscore[1])
                                            {
                                                pass = true;
                                                whacrends[10].enabled = false;
                                                whacdisps[1].text = whacscore[0].ToString();
                                                Audio.PlaySoundAtTransform("PassCheer", transform);
                                                whacdisps[2].text = "";
                                                whacdisps[3].text = "NEW RECORD!";
                                                whacrends[9].enabled = false;
                                                for(int i = 0; i < 9; i++)
                                                {
                                                    whacrends[i].material = whacmats[7];
                                                    Vector3 p = whacelements[i].localPosition;
                                                    whacelements[i].localPosition = new Vector3(p.x, p.y, whacz[((i / 3) * 2) + 1]);
                                                }
                                                yield return new WaitForSeconds(0.1f);
                                                StartCoroutine("WhacWin", 6);
                                                yield return new WaitForSeconds(0.2f);
                                                StartCoroutine("WhacWin", 3);
                                                StartCoroutine("WhacWin", 7);
                                                yield return new WaitForSeconds(0.2f);
                                                StartCoroutine("WhacWin", 0);
                                                StartCoroutine("WhacWin", 4);
                                                StartCoroutine("WhacWin", 8);
                                                yield return new WaitForSeconds(0.2f);
                                                StartCoroutine("WhacWin", 1);
                                                StartCoroutine("WhacWin", 5);
                                                yield return new WaitForSeconds(0.2f);
                                                StartCoroutine("WhacWin", 2);
                                            }
                                        }
                                    }
                                    else
                                        Audio.PlaySoundAtTransform("SFXWhiff", transform);
                                    yield return new WaitForSeconds(0.1f);
                                    if (whacscore[0] <= whacscore[1] && !irredeemablefailure)
                                    {
                                        whacswing = false;
                                        whacrends[9].material = whacmats[9];
                                    }
                                    else
                                        whacrends[9].enabled = false;
                                    break;
                                }
                                else
                                    break;
                            case 3:
                                if (whacpos[0] / 3 < 2)
                                    whacpos[0] += 3;
                                break;
                            case 4:
                                if (whacpos[0] % 3 < 2)
                                    whacpos[0]++;
                                break;
                        }
                        break;
                        ////////////////////////////////////////////////////////////////////////////Boss
                    case 12:
                        Transform kid = bosselements[3].transform;
                        Renderer krend = bossrends[3];
                        switch (b)
                        {
                            case 0:
                                while (bossmovement[1])
                                    yield return null;
                                if (!bossmovement[0])
                                {
                                    float el = 0;
                                    bossmovement[0] = true;
                                    bossfaceleft[0] = true;
                                    kid.localScale = new Vector3(-0.08f, 1, 0.08f);
                                    while (bossmovement[0] && game > 0)
                                    {
                                        el += Time.deltaTime;
                                        el %= 0.2f;
                                        kid.localPosition += new Vector3(Time.deltaTime * 4, 0, 0);
                                        if (kid.localPosition.x > 4.15f)
                                        {
                                            float k = kid.localPosition.z;
                                            kid.localPosition = new Vector3(4.15f, 0, k);
                                        }
                                        if (bossjumps > 1)
                                            krend.material = bossmats[el > 0.1f ? 4 : 3];
                                        yield return null;
                                    }
                                    if (!bossmovement[1] && bossjumps > 1)
                                        krend.material = bossmats[2];
                                }
                                break;
                            case 1:
                                if (bossjumps > 0)
                                {
                                    bossjumps--;
                                    int jump = bossjumps;
                                    Audio.PlaySoundAtTransform("SFXKidJump" + jump, kid);
                                    float upspeed = 2f;
                                    krend.material = bossmats[5];
                                    while (kid.localPosition.z <= 2.7f)
                                    {
                                        if (bossjumps != jump || game < 1)
                                            yield break;
                                        kid.localPosition -= new Vector3(0, 0, upspeed * Time.deltaTime * 7);
                                        if (upspeed > -2f)
                                        {
                                            upspeed -= Time.deltaTime * 7;
                                            if (upspeed < 0)
                                                krend.material = bossmats[6];
                                        }                                        
                                        yield return null;
                                    }
                                    float k = kid.localPosition.x;
                                    kid.localPosition = new Vector3(k, 0, 2.7f);
                                    bossjumps = 2;
                                    if (!bossmovement[0] && bossjumps > 1)
                                        krend.material = bossmats[2];
                                }
                                break;
                            case 2:
                                Transform boss = bosselements[4].transform;
                                if (!bossshoot[0])
                                {
                                    bossshoot[0] = true;
                                    bossshoot[1] = true;
                                    while (bossshoot[1] && game > 0)
                                    {
                                        if (Mathf.Abs(kid.localPosition.z - boss.localPosition.z) < 1 && bossfaceleft[0] == kid.localPosition.x < boss.localPosition.x)
                                        {
                                            if (bossdamage[1])
                                                Audio.PlaySoundAtTransform("SFXKidShootDeflect", boss);
                                            else
                                            {
                                                Audio.PlaySoundAtTransform("SFXKidShootHit", boss);
                                                bossstability--;
                                                BGM.pitch += 1 / 4000f;
                                                if (bossstability < 1)
                                                {
                                                    StartCoroutine("Victory");
                                                    yield break;
                                                }
                                            }
                                        }
                                        else
                                            Audio.PlaySoundAtTransform("SFXKidShootMiss", kid);
                                        yield return new WaitForSeconds(0.15f);
                                    }
                                    bossshoot[0] = false;
                                }
                                break;
                            case 4:
                                while (bossmovement[0])
                                    yield return null;
                                if (!bossmovement[1])
                                {
                                    float er = 0;
                                    bossmovement[1] = true;
                                    bossfaceleft[0] = false;
                                    kid.localScale = new Vector3(0.08f, 1, 0.08f);
                                    while (bossmovement[1] && game > 0)
                                    {
                                        er += Time.deltaTime;
                                        er %= 0.2f;
                                        kid.localPosition -= new Vector3(Time.deltaTime * 4, 0, 0);
                                        if (kid.localPosition.x < -4.1f)
                                        {
                                            float k = kid.localPosition.z;
                                            kid.localPosition = new Vector3(-4.1f, 0, k);
                                        }
                                        if (bossjumps > 1)
                                            krend.material = bossmats[er > 0.1f ? 4 : 3];
                                        yield return null;
                                    }
                                    if (!bossmovement[0] && bossjumps > 1)
                                        krend.material = bossmats[2];
                                }
                                break;
                        }
                        break;
                }
                if(irredeemablefailure && lives < 2)
                    disptexts[2].text = "GAME OVER";
            }
        }
        yield break;
    }

    private IEnumerator ButtonUp(int b)
    {
        if (!moduleSolved)
        {
            Vector3 xz = bts[b].localPosition;
            bts[b].localPosition = new Vector3(xz.x, 0.0391f, xz.z);
            if (phase > 0)
            {
                switch (game)
                {
                    case 4:
                        assamoving[b % 2] = false;
                        break;
                    case 10:
                        if (b == 0 || b == 4)
                            silrot[0] = false;
                        else if (b != 2)
                            silrot[1] = false;
                        break;
                    case 11:
                        switch (b)
                        {
                            case 0:
                                if (whacpos[0] % 3 < 1)
                                    whacpos[0]++;
                                break;
                            case 1:
                                if (whacpos[0] / 3 < 1)
                                    whacpos[0] += 3;
                                break;
                            case 3:
                                if (whacpos[0] / 3 > 1)
                                    whacpos[0] -= 3;
                                break;
                            case 4:
                                if (whacpos[0] % 3 > 1)
                                    whacpos[0]--;
                                break;
                        }
                        break;
                        ////////////////////////////////////////////////////Boss
                    case 12:
                        switch (b)
                        {
                            case 0:
                                bossmovement[0] = false;
                                break;
                            case 2:
                                bossshoot[1] = false;
                                break;
                            case 4:
                                bossmovement[1] = false;
                                break;
                        }
                        break;
                }
            }
        }
        yield break;
    }

    private IEnumerator Wait()
    {
        while (info.GetSolvedModuleNames().Count() < 1)
            yield return new WaitForSeconds(1);
        phase = 1;
        StartCoroutine("Begin");
    }

    private IEnumerator Begin()
    {
        smallscreens[0].SetActive(false);
        smallscreens[1].SetActive(true);
        float e = 0f;
        Vector3[] gpos = new Vector3[16];
        for (int i = 0; i < 16; i++)
            gpos[i] = glitchbm[i].localPosition;
        Audio.PlaySoundAtTransform("GameStart", transform);
        while (e < 2f)
        {
            e += Time.deltaTime;
            for (int i = 0; i < 16; i++)
                glitchbm[i].localPosition = new Vector3(Random.Range(-1.56f, 1.56f), gpos[i].y, gpos[i].z);
            yield return null;
        }
        smallscreens[1].SetActive(false);
        yield return new WaitForSeconds(1);
        smallscreens[2].SetActive(true);
        screens[0].SetActive(true);
        Audio.PlaySoundAtTransform("GameNext", transform);
        StartCoroutine("StageDisplay");
        StartCoroutine("LiftAnim");
        StartCoroutine("BMAnim");
        backlight.enabled = true;
        yield return new WaitForSeconds(1.5f);
        pass = false;
        game = Random.Range(1, prompts.Length + 1);
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Force Spawn
        //
        StartCoroutine("Game" + game.ToString());
        StartCoroutine("Timer");
        disptexts[1].text = prompts[game - 1].Replace("\n", " ");
        while (e < 2.5f)
        {
            disppos[1].localScale = new Vector3(0.0003f, 0.0003f, 1) * (2 - ((e - 2) * 2));
            e += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator StageDisplay()
    {
        float e = 0;
        disptexts[0].text = (stage < 10 ? "0" : "") + stage.ToString();
        while (e < 0.5f)
        {
            disppos[0].localPosition = new Vector3(0, 0.33f, Mathf.Lerp(-0.52f, -0.279f, e * 2));
            e += Time.deltaTime;
            yield return null;
        }
        while(e < 0.75f)
        {
            disptexts[0].fontSize = (int)Mathf.Lerp(180, 300, (e - 0.5f) * 4);
            e += Time.deltaTime;
            yield return null;
        }
        stage++;
        disptexts[0].text = (stage < 10 ? "0" : "") + stage.ToString();
        while (e < 1)
        {
            disptexts[0].fontSize = (int)Mathf.Lerp(300, 180, (e - 0.75f) * 4);
            e += Time.deltaTime;
            yield return null;
        }
        while (e < 1.5f)
        {
            disppos[0].localPosition = new Vector3(0, 0.33f, Mathf.Lerp(-0.279f, -0.52f, (e - 1f) * 2));
            e += Time.deltaTime;
            yield return null;
        }
        disptexts[0].text = "";
    }

    private IEnumerator LiftAnim()
    {
        int p = 0;
        while(game < 1)
        {
            lift[0].material = liftmats[p];
            p += Random.Range(1, 4);
            p %= 4;
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator BMAnim()
    {
        lift[1].enabled = true;
        float e = 0;
        if (pass != null)
        {
            if(!moduleSolved)
                smallscreens[2].SetActive(true);
            if (pass == true)
            {
                Audio.PlaySoundAtTransform("GameSuccess", transform);
                for(int i = 0; i < 40; i++)
                {
                    int c = i % 6 < 4 ? i % 6 : (5 - (i % 6));
                    lift[1].material = liftmats[c + 12];
                    yield return new WaitForSeconds(1 / 23f);
                }
                if (moduleSolved)
                {
                    Audio.PlaySoundAtTransform("GameSolve", transform);
                    for (int i = 0; i < 60; i++)
                    {
                        int c = i % 6 < 4 ? i % 6 : (5 - (i % 6));
                        lift[1].material = liftmats[c + 12];
                        yield return new WaitForSeconds(1 / 23f);
                    }
                    Audio.PlaySoundAtTransform("GameOff", transform);
                    e = 0.4f;
                    float z = screens[0].transform.localScale.z;
                    while(e > 0.2f)
                    {
                        e -= Time.deltaTime;
                        screens[0].transform.localScale = new Vector3(0.1073488f, 0.01f, z * (e - 0.2f) * 5f);
                        yield return null;
                    }
                    float x = screens[0].transform.localScale.x;
                    while (e > 0)
                    {
                        e -= Time.deltaTime;
                        screens[0].transform.localScale = new Vector3(x * e * 5, 0.01f, 0.0001f);
                        yield return null;
                    } 
                    screens[0].SetActive(false);
                    StopCoroutine("LiftAnim");
                    module.HandlePass();
                }
            }
            else
            {
                Audio.PlaySoundAtTransform("GameFail", transform);
                lift[1].material = liftmats[16];
                yield return new WaitForSeconds(2);
                lives--;
                disptexts[3].text = (lives < 10 ? "0" : "") + lives.ToString();
                if (lives == 1)
                {
                    Audio.PlaySoundAtTransform("GameCrack", transform);
                    crack.material = crackmat;
                    module.GetComponent<KMSelectable>().AddInteractionPunch(-2);
                }
                else if(lives < 1)
                {
                    Audio.PlaySoundAtTransform("GameOver", transform);
                    lift[1].material = liftmats[8];
                    smallscreens[3].SetActive(true);
                    while(e < 0.5f)
                    {
                        e += Time.deltaTime;
                        smallscreens[3].transform.localPosition = new Vector3(0, 0.6f, 0.826f * (0.5f - e));
                        yield return null;
                    }
                    smallscreens[2].SetActive(false);
                    disptexts[2].text = "GAME OVER";
                    yield return new WaitForSeconds(0.2f);
                    while(e < 0.8f)
                    {
                        e += Time.deltaTime;
                        float s = Mathf.Lerp(1, 3, (e - 0.5f) / 0.3f);
                        smallscreens[3].transform.localScale = new Vector3(0.025f * s, 1, 0.035f * s);
                        yield return null;
                    }
                    screens[0].SetActive(false);
                    screenglass[0].SetActive(false);
                    screenglass[1].SetActive(true);
                    disptexts[2].text = "";
                    Audio.PlaySoundAtTransform("GameSmash", transform);
                    StartCoroutine("Death");
                    while (e < 0.9f)
                    {
                        e += Time.deltaTime;
                        float s = (e - 0.8f) / 0.1f;
                        hand.localPosition = new Vector3(0, Mathf.Lerp(-0.552f, -0.038f, s), Mathf.Lerp(-0.089f, 1.2f, s));
                        yield return null;
                    }
                    StopCoroutine("LiftAnim");
                    yield break;
                }
            }
        }        
        int f = 0;
        while(game < 1)
        {
            lift[1].material = liftmats[f + 8];
            f += Random.Range(1, 4);
            f %= 4;
            yield return new WaitForSeconds(0.2f);
        }
        lift[1].enabled = false;
        if(phase < 4)
            screens[game].SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            lift[0].material = liftmats[i + 4];
            yield return new WaitForSeconds(0.05f);
        }
        lift[0].enabled = false;
        while(e < 0.5f)
        {
            disppos[1].localPosition = new Vector3(0, 0.0266f, 0.05f * e);
            e += Time.deltaTime;
            yield return null;
        }
        disptexts[1].text = "";
        if (phase < 4)
        {
            disptexts[2].text = prompts[game - 1];
            smallscreens[2].SetActive(false);
            screens[0].SetActive(false);
        }
    }

    private IEnumerator Next()
    {
        game = 0;
        disppos[1].localPosition = new Vector3(0, 0.0266f, 0);
        screens[0].SetActive(true);
        lift[0].enabled = true;
        disptexts[2].text = "";
        for (int i = 0; i < 4; i++)
        {
            lift[0].material = liftmats[7 - i];
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("LiftAnim");
        StartCoroutine("BMAnim");
        if (!moduleSolved)
        { 
            yield return new WaitForSeconds(2);
            if (lives > 0)
            {
                float prog = ((float)(stage + info.GetSolvedModuleNames().Count(x => !exempt.Contains(x))) / limit) + 1;
                float e = 0;
                if (prog > speed + 0.25f)
                {
                    phase++;
                    speed += 0.25f;
                    BGM.pitch = speed;
                    if (phase > 3)
                    {
                        Audio.PlaySoundAtTransform("GameBoss", transform);
                        disppos[0].localPosition = new Vector3(0, 0.33f, -0.242f);
                        disptexts[0].text = "BOSS STAGE";
                        for (int i = 0; i < 4; i++)
                        {
                            disptexts[0].fontSize = 220;
                            yield return new WaitForSeconds(0.4f);
                            disptexts[0].fontSize = 180;
                            yield return new WaitForSeconds(0.6f);
                        }
                    }
                    else
                    {
                        Audio.PlaySoundAtTransform("GameSpeedUp", transform);
                        disptexts[0].text = "SPEED UP";
                        while (e < 0.5f)
                        {
                            disppos[0].localPosition = new Vector3(0, 0.33f, Mathf.Lerp(-0.52f, -0.27f, e * 2));
                            e += Time.deltaTime;
                            yield return null;
                        }
                        yield return new WaitForSeconds(2.5f);
                        while (e > 0)
                        {
                            disppos[0].localPosition = new Vector3(0, 0.33f, Mathf.Lerp(-0.52f, -0.27f, e * 2));
                            e -= Time.deltaTime;
                            yield return null;
                        }
                        disptexts[0].text = "";
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                StartCoroutine("StageDisplay");
                Audio.PlaySoundAtTransform("GameNext", transform);
                irredeemablefailure = false;
                pass = false;
                yield return new WaitForSeconds(1.5f);
                e = 0;
                game = Random.Range(1, prompts.Length + 1);
                if (phase < 4)
                {
                    StartCoroutine("Timer");
                    StartCoroutine("Game" + game.ToString());
                    disptexts[1].text = prompts[game - 1];

                    while (e < 0.5f)
                    {
                        disppos[1].localScale = new Vector3(0.0003f, 0.0003f, 1) * (2 - (e * 2));
                        e += Time.deltaTime;
                        yield return null;
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.4f);
                    BGM.clip = bgms[prompts.Length];
                    BGM.pitch = 1;
                    BGM.loop = true;
                    BGM.Play();
                    screens[prompts.Length + 1].SetActive(true);
                    bombtimer[0].enabled = true;
                    for (int i = 8; i >= 0; i--)
                    {
                        bombtimer[0].material = bombmats[i];
                        yield return new WaitForSeconds(0.5f);
                    }
                    bombtimer[0].enabled = false;
                    bosselements[1].SetActive(true);
                    Audio.PlaySoundAtTransform("SFXTauntLaugh", transform);
                    yield return new WaitForSeconds(1.75f);
                    Audio.PlaySoundAtTransform("SFXJumpSuper", transform);
                    bossrends[0].material = bossmats[0];
                    e = 0;
                    while (e < 0.25f)
                    {
                        e += Time.deltaTime;
                        bosselements[1].transform.localPosition = new Vector3(-3.69f, 0.001f, Mathf.Lerp(-3.21f, 4.2f, 4 * e));
                        yield return null;
                    }
                    bosselements[1].SetActive(false);
                    bosselements[0].SetActive(true);
                    game = prompts.Length + 1;
                    yield return new WaitForSeconds(0.75f);
                    bossrends[4].enabled = true;
                    while (e > 0)
                    {
                        e -= Time.deltaTime;
                        bosselements[4].transform.localPosition = new Vector3(-3.55f, 0, Mathf.Lerp(2, -4, 4 * e));
                        yield return null;
                    }
                    bosselements[4].transform.localPosition = new Vector3(-3.55f, 0, 2);
                    bossrends[4].material = bossmats[7];
                    bossrends[1].material = bossmats[1];
                    bossfire.Play();
                    yield return new WaitForSeconds(1);
                    bossattack = Random.Range(0, 3);
                    StartCoroutine("BossAttack");
                    StartCoroutine(PlayerProximity(bosselements[4], 0.7f));
                }
            }
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.45f);
        BGM.clip = bgms[game - 1];
        BGM.Play();
        yield return new WaitForSeconds((bgms[game - 1].length - 4.7f) / speed);
        bombtimer[0].enabled = true;
        for(int i = 8; i >= 0; i--)
        {
            bombtimer[0].material = bombmats[i];
            yield return new WaitForSeconds(0.5f / speed);
        }
        bombtimer[0].enabled = false;
        bombtimer[1].enabled = true;
        bombtimer[1].material = bombmats[9];
        yield return new WaitForSeconds(0.1f);
        bombtimer[1].material = bombmats[10];
        yield return new WaitForSeconds(0.1f);
        bombtimer[1].enabled = false;
        screens[game].SetActive(false);
        StartCoroutine("Next");
    }

    private IEnumerator Death()
    {
        float e = 0;
        Audio.PlaySoundAtTransform("OopsMySystemCrashed", transform);
        Vector3 campos = mainCamera.transform.localPosition;
        Camera cam = mainCamera.GetComponent<Camera>();
        pixelimage.SetActive(true);
        mainCamera.GetComponent<Camera>().targetTexture = pixelate;
        float[] r = new float[4];
        while (e < 2)
        {
            e += Time.deltaTime;
            for (int i = 0; i < 3; i++)
                r[i] = Random.Range(-e, e);
            mainCamera.transform.localPosition = campos + new Vector3(r[0] / 250, r[1] / 250, 0);
            cam.ResetProjectionMatrix();
            var m = cam.projectionMatrix;
            m.m00 *= 1 + (r[2] / 3);
            m.m11 /= m.m00;
            cam.projectionMatrix = m;
            int p = Mathf.Max(1, (int)((2 - e) * 512));
            pixelate.Release();
            pixelate.width = p;
            pixelate.height = p;
            pixelate.Create();
            yield return null;
        }
        if (!Application.isEditor)
            Application.Quit();
    }

    private IEnumerator Game1()
    { 
        arrowcount = 0;
        for (int i = 0; i < 5; i++)
            arrowrends[i].enabled = i < 3;
        arrowt[1].localScale = new Vector3(1, 1, 1);
        arrowt[1].localEulerAngles = new Vector3(0, 180, 0);
        int[] m = new int[2] { Random.Range(0, 2), Random.Range(0, 2)};
        arrowrends[0].material = arrowmats[m[0]];
        arrowrends[1].material = arrowmats[m[1] + 2];
        arrowrends[2].material = arrowmats[m[1] + 4];
        int a = Random.Range(0, 4);
        arrowinstr = 0;
        arrowans = new int[4] { 1, 4, 3, 0 }[a];
        arrowt[0].localEulerAngles = new Vector3(0, 90 * a, 0);
        int e = 0;
        while(game > 0)
        {
            yield return new WaitForSeconds(0.3f / speed);
            m[1] ^= 1;
            arrowrends[1].material = arrowmats[m[1] + 2];
            arrowrends[2].material = arrowmats[m[1] + (2 * (arrowinstr + 2))];
            e++;
            if(e % 3 == 0)
            {
                m[0] ^= 1;
                arrowrends[0].material = arrowmats[m[0]];
            }
        }
    }

    private IEnumerator Game2()
    {
        avonum = 0;
        avoelements[0].SetActive(true);
        avoelements[1].SetActive(false);
        avorends[0].enabled = true;
        for(int i = 0; i < 2; i++)
            avopos[i] = 2;
        int a = new int[] { 0, 1, 3, 4, 5, 9, 15, 19, 20, 21, 23, 24 }.PickRandom();
        avospawn[0] = a / 5;
        avospawn[1] = a % 5;
        avoelements[2].transform.localPosition = new Vector3(-0.02f, 0.0012f, 0.02f);
        avoelements[3].transform.localPosition = new Vector3((avospawn[1] * 0.944f) - 1.908f, 0.001f, 2.68f - (avospawn[0] * 1.33f));
        avorends[1].material = avomats[0];
        avorends[2].material = avomats[Random.Range(7, 14)];
        for (int i = 3; i < 23; i++)
            avorends[i].material = avomats[14];
        for (int i = 23; i < 43; i++)
            avorends[i].enabled = false;
        while (avonum < 1)
        {
            if (game < 1)
                yield break;
            yield return null;
        }
        avoshoot = Enumerable.Range(0, 20).ToArray().Shuffle().Take(5).ToArray();
        for (int i = 0; i < 5; i++)
        {
            StartCoroutine("Avoshoot", i);
            yield return new WaitForSeconds(1 / speed);
        }
    }

    private IEnumerator Avoshoot(int z)
    {
        while (!irredeemablefailure && game > 0 && avonum < 6)
        {
            int x = avoshoot[z];
            float wait = Random.Range(0.4f, 1f);
            for (int i = 0; i < 5; i++)
            {
                if (irredeemablefailure || game < 1 || avonum > 5)
                    yield break;
                avorends[x + 3].material = avomats[14 + i];
                yield return new WaitForSeconds(wait / speed);
            }
            for (int i = 0; i < 6; i++)
            {
                if (irredeemablefailure || game < 1 || avonum > 5)
                    yield break;
                avorends[x + 3].material = avomats[14 + (i % 2) * 5];
                yield return new WaitForSeconds(0.05f);
            }
            Audio.PlaySoundAtTransform("SFXLaser", transform);
            avorends[x + 23].material = avomats[20];
            avorends[x + 23].enabled = true;
            if (x % 5 == avopos[1 - ((x % 10) / 5)])
            {
                irredeemablefailure = true;
                Audio.PlaySoundAtTransform("FailDeathScream", transform);
                avorends[1].material = avomats[1];
                if(lives < 2)
                    disptexts[2].text = "GAME OVER";
                yield return new WaitForSeconds(0.4f);
                for (int i = 2; i < 7; i++)
                {
                    avorends[1].material = avomats[i];
                    yield return new WaitForSeconds(0.1f);
                }
            }
            for (int i = 20; i < 24; i++)
            {
                if (irredeemablefailure || game < 1 || avonum > 5)
                    yield break;
                avorends[x + 23].material = avomats[i];
                yield return new WaitForSeconds(0.1f);
            }
            avorends[x + 23].enabled = false;
            avorends[x + 3].material = avomats[14];
            avoshoot[z] = Enumerable.Range(0, 20).Where(q => !avoshoot.Contains(q)).PickRandom();
        }
    }

    private IEnumerator Game3()
    {
        stackelements[0].SetActive(true);
        stackrends[13].enabled = false;
        stackelements[1].SetActive(false);
        stackmoving = false;
        do {
            int[] stackinit = Enumerable.Range(0, 12).Select(x => x / 3).ToArray().Shuffle().ToArray();
            for (int i = 0; i < 4; i++)
            {
                stacks[i].Clear();
                for (int j = 0; j < 3; j++)
                {
                    int s = stackinit[(3 * i) + j];
                    if (s > 0)
                        stacks[i].Add(s - 1);
                }
            }
        } while (stacks.Any(x => x.Distinct().Count() < 2 && x.Count() > 1));
        stackpos[1] = -1;
        stackrends[0].enabled = false;
        stackpos[0] = Random.Range(0, 4);
        stackpos[1] = -1;
        stackmag[0].localPosition = new Vector3(new float[4] { 3.68f, 1.22666f, -1.2266f, -3.68f}[stackpos[0]], 0.0008f, -3.9f);
        stackmag[1].localPosition = new Vector3(0, -0.0002f, 0.16f);
        stackmag[1].localScale = new Vector3(0.03f, 1, 0);
        stackmag[2].localPosition = new Vector3(0, 0.0003f, 0.39f);
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 3; j++)
            {
                int s = (3 * i) + j;
                if (j >= stacks[i].Count())
                    stackrends[s + 1].enabled = false;
                else
                {
                    stackrends[s + 1].material = stackmats[stacks[i][j]];
                    stackrends[s + 1].enabled = true;
                }
            }
        yield break;
    }

    private IEnumerator StackPass()
    {
        stackdisp.text = "";
        stackrends[13].enabled = false;
        stackelements[1].SetActive(true);
        float e = 0;
        while (e < 1)
        {
            e += Time.deltaTime * speed * 3;
            stackelements[2].transform.localPosition = new Vector3(Mathf.Lerp(-4.3f, -2.1f, e), 0.0012f, Mathf.Lerp(3.6f, 1.4f, e));
            stackelements[2].transform.localScale = new Vector3(Mathf.Lerp(0.3f, 0.6f, e), 1, Mathf.Lerp(0.4f, 0.8f, e));
            yield return null;
        }
        stackelements[0].SetActive(false);
        stackrends[13].enabled = true;
        stackdisp.text = new string[] { "GOOD JOB\nTODAY", "STACK-TASTIC\nWORK", "EXCELLENT\nSTACKING", "YOU ARE\nGREAT", "YOU DID A\nSUPERB JOB"}.PickRandom();
        int p = 1;
        while(game > 0)
        {
            p ^= 1;
            stackrends[13].material = stackmats[p + 3];
            yield return new WaitForSeconds(0.75f / speed);
        }
    }

    private IEnumerator Game4()
    {
        int fselect = Random.Range(0, 3);
        int[] arr = new int[7] { 0, 1, 2, 3, 4, 5, 6 };
        for (int i = 0; i < 3; i++)
        {
            arr = arr.Shuffle();
            for (int j = 0; j < 7; j++)
                assarends[(i * 7) + j + 14].material = assamats[(i * 7) + arr[j]];
            if (i == fselect)
                assarends[43].material = assamats[(i * 7) + arr[0]];
        }
        for (int i = 0; i < 7; i++)
        {
            assareveal[i] = false;
            assarends[i].enabled = true;
        }
        assarends[42].enabled = false;
        for (int i = 0; i < 7; i++)
        {
            float[] cpos = new float[2] { Random.Range(-4f, 4f), Random.Range(-1.64f, 1.78f)};
            for (int j = 1; j < 6; j++)
                assarends[(j * 7) + i].enabled = false;
            AssaPos(i, cpos[0], cpos[1]);
            StartCoroutine(AssaMove(i));
        }
        yield break;
    }

    private void AssaPos(int n, float x, float z)
    {
        float y = 0.0005f + ((z + 1.64f) / 1000);
        float s = 0.1f + ((z + 1.64f) / 68.4f);
        assaelements[n].transform.localPosition = new Vector3(x, y, z);
        assaelements[n].transform.localScale = new Vector3(s, 1, 2 * s);
        AssaCheck(n, x, z, s);
    }

    private void AssaCheck(int n, float x, float z, float s)
    {
        bool r = assacross[0] > x - (s * 4) && assacross[0] < x + (s * 4) && assacross[1] > z - (s * 9) && assacross[1] < z + (s * 9);
        if (r ^ assareveal[n])
        {
            assareveal[n] ^= true;
            for (int i = 0; i < 5; i++)
                assarends[(7 * i) + n].enabled ^= true;
            if (assareveal[n])
                Debug.Log(n);
        }
    }

    private IEnumerator AssaSkidaddle(int n, float x)
    {
        int r = x < assaelements[n].transform.localPosition.x ? 1 : -1;
        float s = Random.Range(4f, 6f);
        while (game > 0 && Mathf.Abs(assaelements[n].transform.localPosition.x) < 4)
        {
            assaelements[n].transform.localPosition += new Vector3(Time.deltaTime * s * r, 0, 0);
            yield return null;
        }
        assarends[n].enabled = false;
    }

    private IEnumerator AssaMove(int n)
    {
        while (game > 0)
        {
            float[] startpos = new float[2] { assaelements[n].transform.localPosition.x, assaelements[n].transform.localPosition.z };
            float[] targetpos = new float[2] { Random.Range(-4f, 4f), Random.Range(-1.64f, 1.78f) };
            float[] currentpos = new float[2] { 0, assaelements[n].transform.localPosition.z };
            float e = 0;
            float m = (Mathf.Abs(startpos[0] - targetpos[0]) / 30f) + (Mathf.Abs(startpos[1] - targetpos[1]) / 8f);
            while (e < 1)
            {
                e += Time.deltaTime * m * speed;
                for (int i = 0; i < 2; i++)
                    currentpos[i] = Mathf.Lerp(startpos[i], targetpos[i], e);
                yield return null;
                if (pass == true || irredeemablefailure)
                    yield break;
                AssaPos(n, currentpos[0], currentpos[1]);
            }
            AssaPos(n, targetpos[0], targetpos[1]);
            e = Random.Range(4f, 4.8f) / speed;
            float s = assaelements[n].transform.localScale.x;
            while(e > 0)
            {
                e -= Time.deltaTime;
                if (pass == true || irredeemablefailure)
                    yield break;
                AssaCheck(n, targetpos[0], targetpos[1], s);
                yield return null;
            }
        }
    }

    private IEnumerator Game5()
    {
        for (int i = 0; i < 2; i++)
            duelbool[i] = false;
        duelrends[0].material = duelmats[0];
        duelelements[0].SetActive(true);
        duelelements[1].SetActive(false);
        duelelements[3].SetActive(false);
        duelelements[4].SetActive(false);
        float[] t = new float[2] { Random.Range(3, (32.5f / speed) - 2.1f), 0f };
        t[1] = (32.5f / speed) - t[0] - 2;
        while(t[0] > 0)
        {
            t[0] -= Time.deltaTime;
            if (duelbool[1])
            {
                t[0] = 0;
            }
            yield return null;
        }
        duelelements[0].SetActive(false);
        t[0] = 0.75f / speed;
        if (!duelbool[1])
        {
            duelbool[0] = true;
            Audio.PlaySoundAtTransform("SFXDon", transform);
            duelelements[1].SetActive(true);
            dueldir = Random.Range(0, 5);
            duelrends[1].material = duelmats[dueldir == 2 ? 8 : 7];
            duelelements[2].transform.localEulerAngles = new Vector3(0, new int[5] { -90, 0, 0, 180, 90}[dueldir], 0);
            while(t[0] > 0)
            {
                t[0] -= Time.deltaTime;
                if (duelbool[1])
                {
                    t[0] = 0;
                }
                yield return null;
            }
            duelbool[0] = false;
            duelelements[1].SetActive(false);
        }
        for(int i = 1; i < 3; i++)
        {
            duelrends[0].material = duelmats[i];
            yield return new WaitForSeconds(0.05f / speed);
        }
        Audio.PlaySoundAtTransform("SFXClash", transform);
        duelrends[2].enabled = true;
        yield return null;
        duelrends[2].enabled = false;
        duelrends[0].material = duelmats[3];
        yield return new WaitForSeconds(0.05f / speed);
        duelrends[0].material = duelmats[4];
        yield return new WaitForSeconds(t[1]);
        Audio.PlaySoundAtTransform("SFXSplatter", transform);
        duelrends[2].enabled = true;
        yield return null;
        duelrends[2].enabled = false;
        duelrends[0].material = duelmats[pass == true ? 5 : 6];
        duelelements[pass == true ? 3 : 4].SetActive(true);
    }

    private IEnumerator Game6()
    {
        ligrends[19].material = ligmats[0];
        ligrends[0].enabled = true;
        int fix = Random.Range(0, 9);
        for(int i = 0; i < 9; i++)
        {
            if (i == fix || Random.Range(0, 2) == 1)
                foreach (int l in Ligtoggle(i))
                    ligio[0, i] ^= true;
            if (i != fix && Random.Range(0, 2) == 1)
                foreach (int l in Ligtoggle(i))
                    ligio[1, i] ^= true;
        }
        for (int i = 0; i < 18; i++)
            ligrends[i + 1].enabled = ligio[i / 9, i % 9];
        yield break;
    }

    private int[] Ligtoggle(int n)
    {
        return new int[9][] { new int[3] { 0, 1, 3}, new int[4] { 0, 1, 2, 4}, new int[3] { 1, 2, 5}, new int[4] { 0, 3, 4, 6}, new int[5] { 1, 3, 4, 5, 7}, new int[4] { 2, 4, 5, 8}, new int[3] { 3, 6, 7}, new int[4] { 4, 6, 7, 8}, new int[3] { 5, 7, 8} }[n];
    }

    private IEnumerator Game7()
    {
        countphase = false;
        coudisps[0].text = "HOW MANY\nCRITTERS\nARE IN\nTHIS BOX?";
        coudisps[1].text = "";
        couhost[0].localPosition = new Vector3(3.09f, 0.0002f, -2.51f);
        couhost[1].localEulerAngles = new Vector3(0, 0, 0);
        for(int i = 0; i < 3; i++)
            courends[42 + i].material = coumats[15 + (3 * i)];
        courends[45].enabled = true;
        StartCoroutine(CouAnim());
        counts[0] = Random.Range(15, 30);
        Debug.Log(counts[0]);
        for(int i = 0; i < 30; i++)
        {
            if (i < counts[0])
            {
                couelements[i].SetActive(true);
                couelements[i].transform.localPosition = new Vector3(Random.Range(-3.9f, 3.9f), 0, Random.Range(-1.2f, 1.2f));
                StartCoroutine(CouCritter(couelements[i].transform, courends[i], Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(0.3f, 1f)));
            }
            else
                couelements[i].SetActive(false);
        }
        float e = 27.11f / (speed * 8);
        courends[40].enabled = true;
        for(int i = 0; i < 8; i++)
        {
            courends[40].material = coumats[i + 2];
            yield return new WaitForSeconds(e);
        }
        countphase = true;
        courends[40].enabled = false;
        coudisps[0].text = "";
        yield return new WaitForSeconds(1);
        for(int i = 0; i < counts[0]; i++)
        {
            couelements[i].transform.localPosition = new Vector3(3.24f - ((i % 10) * 0.72f), 0.0002f, ((i / 10) * 0.75f) - 0.75f);
            coudisps[1].text = ((i + 1) < 10 ? "0" : "") + (i + 1).ToString();
            yield return new WaitForSeconds(3.5f / (counts[0] * speed));
        }
        if(counts[1] == counts[0])
        {
            pass = true;
            Audio.PlaySoundAtTransform("PassFanfare5", transform);
            courends[43].material = coumats[19];
        }
        else
        {
            irredeemablefailure = true;
            Audio.PlaySoundAtTransform("FailBuzzer", transform);
            if(lives < 2)
                disptexts[2].text = "GAME OVER";
            courends[42].material = coumats[17];
            courends[43].material = coumats[20];
            courends[45].enabled = false;
        }
    }

    private IEnumerator CouCritter(Transform c, Renderer r, float x, float y, float z)
    {
        float e = Random.Range(0, 2 * z);
        while (!countphase)
        {
            e += Time.deltaTime * speed;
            e %= 2 * z;
            r.material = coumats[(int)e];
            if (Mathf.Abs(c.localPosition.x) > 3.9f)
                x *= -1;
            if (Mathf.Abs(c.localPosition.z) > 1.2f)
                y *= -1;
            c.localPosition += new Vector3(x, 0, y) * speed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator CouAnim()
    {
        float[] a = new float[3] { Random.Range(0f, Mathf.PI * 2), Random.Range(0f, Mathf.PI * 2), Random.Range(0f, 2f)};
        int b = Random.Range(0, 5);
        while (!countphase)
        {
            for(int i = 0; i < 3; i++)
                a[i] += Time.deltaTime * speed * (9 - (3 * i));
            a[2] %= 2;
            couhost[0].localPosition = new Vector3(3.09f + (Mathf.Sin(a[0]) / 10), 0.0004f, -2.51f);
            couhost[1].localEulerAngles = new Vector3(0, 5 * Mathf.Sin(a[0]), 0);
            couhost[2].localPosition = new Vector3(0, 0.0002f, 0.46f * Mathf.Sin(a[1]));
            courends[44].material = coumats[21 + (int)a[2]];
            b += Random.Range(1, 5);
            b %= 5;
            courends[41].material = coumats[b + 10];
            coudisps[0].transform.localPosition = new Vector3(Random.Range(-0.15f, 0.15f), 0.0002f, Random.Range(-3.5f, -3.2f));
            yield return null;
        }
        courends[44].material = coumats[23];
        a[0] = 0;
        float e = 0;
        while(e <= 1)
        {
            e += Time.deltaTime;
            couhost[0].localPosition = new Vector3( Mathf.Lerp(3.09f, 0.4f, e * (2 - e)), 0.0004f, -2.51f);
            couhost[1].localEulerAngles = new Vector3( 0, Mathf.Lerp(0, -15, 4 * e * (1 - e)), 0);
            b += Random.Range(1, 5);
            b %= 5;
            courends[41].material = coumats[b + 10];
            yield return null;
        }
        while (game > 0)
        {
            for (int i = 0; i < 3; i++)
                a[i] += Time.deltaTime * speed * (9 - (3 * i));
            a[2] %= 2;
            couhost[0].localPosition = new Vector3(0.4f + (Mathf.Sin(a[0]) / 10), 0.0004f, -2.51f);
            couhost[1].localEulerAngles = new Vector3(0, 5 * Mathf.Sin(a[0]), 0);
            couhost[2].localPosition = new Vector3(0, 0.0002f, 0.46f * Mathf.Sin(a[1]));
            courends[44].material = coumats[irredeemablefailure ? 27 : ((pass == true ? 25 : 23) + (int)a[2])];
            b += Random.Range(1, 5);
            b %= 5;
            courends[41].material = coumats[b + 10];
            yield return null;
        }
    }

    private IEnumerator Game8()
    {
        mashelements[0].SetActive(true);
        mashelements[5].SetActive(false);
        mashcount = 250;
        mashtext.text = "250";
        mashrends[0].material = mashmats[8];
        mashrends[2].material = mashmats[13];
        mashelements[4].transform.localScale = new Vector3(1, 1, 1);
        int e = Random.Range(0, 4);
        while(game > 0)
        {
            mashelements[2].transform.localScale = new Vector3(1.2f * ((2 * Random.Range(0, 2)) - 1), 1, 1.2f * ((2 * Random.Range(0, 2)) - 1));
            e += Random.Range(1, 4);
            e %= 4;
            mashrends[1].material = mashmats[e + 9];
            yield return new WaitForSeconds(0.15f / speed);
        }
    }

    private IEnumerator Game9()
    {
        snakelements[18].SetActive(true);
        snakelements[19].SetActive(false);
        snake = new List<int> { Random.Range(0, 77)};
        snakdice.Clear();
        snaklength = 7;
        snakdir = -1;
        snakturn = false;
        for (int i = 1; i < 12; i++)
            snakrends[i].enabled = false;
        SnakPlace(snakelements[0].transform, snake[0]);
        snakelements[0].transform.localEulerAngles = new Vector3(0, 180, 0);
        snakrends[0].material = snakmats[0];
        for(int i = 0; i < 6; i++)
        {
            int r = Random.Range(0, 77);
            while(SnakAdj(r, snake[0]) || snakdice.Any(x => SnakAdj(r, x)))
                r = Random.Range(0, 77);
            snakdice.Add(r);
            snakrends[i + 12].enabled = true;
            SnakPlace(snakelements[i + 12].transform, snakdice[i]);
        }
        Debug.Log(string.Join(", ", snakdice.Select(x => x.ToString()).ToArray()));
        while(game > 0)
        {
            if (snakdir >= 0)
            {
                snakturn = false;
                int s = snake[0];
                if (new bool[] { s % 11 < 1, s < 11, false, s >= 66, s % 11 > 9 }[snakdir])
                {
                    StartCoroutine("SnakFail");
                    yield break;
                }
                if (snake.Count() < snaklength)
                {
                    snake.Add(snake.Last());
                    snakrends[snake.Count() - 1].enabled = true;
                    SnakPlace(snakelements[snake.Count() - 1].transform, snake.Last());
                }
                for (int i = snake.Count() - 1; i > 0; i--)
                {
                    snake[i] = snake[i - 1];
                    SnakPlace(snakelements[i].transform, snake[i]);
                }
                switch (snakdir)
                {
                    case 0: snake[0]--; snakelements[0].transform.localEulerAngles = new Vector3(0, -90, 0); break;
                    case 1: snake[0] -= 11; snakelements[0].transform.localEulerAngles = new Vector3(0, 0, 0); break;
                    case 3: snake[0] += 11; snakelements[0].transform.localEulerAngles = new Vector3(0, 180, 0); break;
                    case 4: snake[0]++; snakelements[0].transform.localEulerAngles = new Vector3(0, 90, 0); break;
                }
                SnakPlace(snakelements[0].transform, snake[0]);
                if (snake.Skip(1).Contains(snake[0]) || snakdice.Skip(1).Contains(snake[0]))
                {
                    StartCoroutine("SnakFail");
                    yield break;
                }
                if (snake[0] == snakdice[0])
                {
                    if (snaklength > 11)
                    {
                        pass = true;
                        snakturn = true;
                        snakelements[18].SetActive(false);
                        snakelements[19].SetActive(true);
                        snakrends[19].material = snakmats[Random.Range(2, 4)];
                        Audio.PlaySoundAtTransform("PassFanfare", transform);
                        float e = 1.2f;
                        while(e > 1)
                        {
                            snakelements[19].transform.localScale = new Vector3(e, 1, e);
                            e -= Time.deltaTime;
                            yield return null;
                        }
                        yield break;
                    }
                    else
                    {
                        snaklength++;
                        snakdice.RemoveAt(0);
                        snakrends[snaklength + 4].enabled = false;
                        Audio.PlaySoundAtTransform("SFXNom", transform);
                    }
                }
            }
            yield return new WaitForSeconds(0.15f / speed);
        }
    }

    private bool SnakAdj(int x, int y)
    {
        if (x == y)
            return true;
        else if (x - y == 1 && x % 11 > 0)
            return true;
        else if (x - y == -1 && x % 11 < 10)
            return true;
        else if (Mathf.Abs(x - y) == 11)
            return true;
        else
            return false;
    }

    private void SnakPlace(Transform d, int p)
    {
        int x = p % 11;
        int y = p / 11;
        d.localPosition = new Vector3((x * 0.749f) - 3.742f, 0, 2.205f - (y * 0.749f));
    }

    private IEnumerator SnakFail()
    {
        Audio.PlaySoundAtTransform("FailCrash", transform);
        snakrends[18].enabled = true;
        yield return new WaitForSeconds(0.03f);
        snakrends[18].enabled = false;
        snakturn = true;
        irredeemablefailure = true;
        if(lives < 2)
            disptexts[2].text = "GAME OVER";
        List<int> sn = new List<int> { 1, 11, -11, -1 };
        snakrends[0].material = snakmats[1];
        for(int i = snake.Count() - 1; i > 0; i--)
        {
            for (int j = 0; j < i; j++)
            {
                snake[j] = snake[j + 1];
                SnakPlace(snakelements[j].transform, snake[j]);
            }
            int s = sn.IndexOf(snake[0] - snake[1]);
            snake.RemoveAt(i);
            snakrends[i].enabled = false;
            if(i > 1)
                 snakelements[0].transform.localEulerAngles = new Vector3(0, new int[] { 90, 180, 0, -90}[s], 0);
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }

    private IEnumerator Game10()
    {
        silphase = false;
        silelements[3].SetActive(false);
        silelements[4].SetActive(false);
        silelements[1].transform.localScale = new Vector3(5.5f, 0.02f, 5.5f);
        sildisps[9].text = "";
        if (silopts.Count() > 0)
            silrends[silopts[9]].enabled = false;
        silopts = Enumerable.Range(0, 64).ToArray().Shuffle().Take(9).ToList();
        int ans = silopts[0];
        silelements[2].transform.localEulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        silrends[ans].enabled = true;
        yield return new WaitForSeconds(42.527f / (2 * speed));
        silphase = true;
        silselect = 4;
        silrends[ans].enabled = false;
        silelements[3].SetActive(true);
        silopts = silopts.Shuffle();
        for (int i = 0; i < 9; i++)
        {
            sildisps[i].text = silnames[silopts[i]];
            sildisps[i].transform.localEulerAngles = new Vector3(90, 180, 0);
            sildisps[i].fontSize = 200;
            sildisps[i].color = new Color(0, 200f / 255, 0);
        }
        silopts.Add(ans);
        StartCoroutine(Silswitch(4));
        int r = Random.Range(0, 4);
        while (silphase && game > 0)
        {
            silrends[64].material = silmats[r];
            r += Random.Range(1, 4);
            r %= 4;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator Silrotate(int h, int v)
    {
        Transform c = silelements[0].transform;
        if (v == 0)
        {
            while (!silphase && silrot[0])
            {
                float t = Time.deltaTime;
                silelements[2].transform.RotateAround(c.position, c.forward, t * h * 180);
                yield return null;
            }
        }
        else
        {
            while (!silphase && silrot[1])
            {
                float t = Time.deltaTime;
                silelements[2].transform.RotateAround(c.position, c.right, t * v * 180);
                yield return null;
            }
        }
    }

    private IEnumerator Silswitch(int x)
    {
        Audio.PlaySoundAtTransform("SFXOption", transform);
        float e = 0;
        while(e < 0.2f)
        {
            e += Time.deltaTime;
            sildisps[x].transform.localEulerAngles = new Vector3(90, 180 - (e * 100), 0);
            sildisps[x].fontSize = 200 + (int)(e * 100);
            sildisps[x].color = new Color(0, e + 0.8f, 0);
            yield return null;
        }
        sildisps[x].transform.localEulerAngles = new Vector3(90, 160, 0);
        sildisps[x].fontSize = 220;
        sildisps[x].color = new Color(0, 1, 0);
    }

    private IEnumerator Game11()
    {
        whacswing = false;
        whacrends[9].enabled = true;
        whacrends[9].material = whacmats[9];
        whacrends[10].enabled = false;
        whacpos[0] = 4;
        whacpos[1] = 4;
        whacelements[9].localPosition = whacpositions[4];
        whacelements[9].localScale = new Vector3(0.12f, 1, 0.2625f);
        whaclimit = Random.Range(4, 7);
        whacscore = new int[2] { 0, (4 * whaclimit) + Random.Range(1, 4)};
        whacdisps[0].text = "";
        whacdisps[1].text = "";
        whacdisps[3].text = "DO NOT WHACK";
        whacforbid = Random.Range(0, whaclimit);
        whacdisps[2].text = "";
        for(int i = 0; i < 9; i++)
        {
            whaccostume[i] = -1;
            Vector3 p = whacelements[i].localPosition;
            whacelements[i].localPosition = new Vector3(p.x, p.y, whacz[((i / 3) * 2) + 1]);
            StartCoroutine("WhacUp", i);
        }
        yield return new WaitForSeconds(0.3f);
        whacdisps[0].text = "00";
        whacdisps[1].text = whacscore[1].ToString();
        whacdisps[2].text = whacnames[whacforbid];
        Debug.Log(whacnames[whacforbid]);
        StartCoroutine("WhacProhibit");
        float[] wobble = new float[4] { Random.Range(0, 2 * Mathf.PI), Random.Range(0, 2 * Mathf.PI), Random.Range(1f, 3) * Mathf.PI, Random.Range(1f, 3) * Mathf.PI};
        while(game > 0 && pass == false && !irredeemablefailure)
        {
            for (int i = 0; i < 2; i++)
                wobble[i] += wobble[i + 2] * Time.deltaTime;
            whacdisps[2].transform.localEulerAngles = new Vector3(90, 180, 20 * Mathf.Sin(wobble[0]));
            whacdisps[2].transform.localScale = new Vector3(0.03f, 0.05f, 1) * ((Mathf.Sin(wobble[1]) / 4) + 0.75f);
            if(whacpos[0] != whacpos[1] && !whacswing)
            {
                Audio.PlaySoundAtTransform("SFXSwish", transform);
                whacpos[1] = whacpos[0];
                whacelements[9].localPosition = whacpositions[whacpos[1]];
                whacelements[10].localPosition = new Vector3(Random.Range(-0.24f, 0.24f), 0.0002f, Random.Range(-3.1f, -2.9f));
            }
            yield return null;
        }
    }

    private IEnumerator WhacProhibit()
    {
        while(game > 0 && pass == false && !irredeemablefailure)
        {
            yield return new WaitForSeconds(Random.Range(6f, 15) / speed);
            if (game < 1 || pass == true || irredeemablefailure)
                yield break;
            whacforbid = 6;
            whacdisps[2].text = "";
            whacdisps[3].text = "";
            whacrends[10].enabled = true;
            Audio.PlaySoundAtTransform("SFXSwitch", transform);
            yield return new WaitForSeconds(0.6f / speed);
            if (game < 1 || pass == true || irredeemablefailure)
                yield break;
            whacforbid += Random.Range(1, whaclimit - 1);
            whacforbid %= whaclimit;
            whacdisps[2].text = whacnames[whacforbid];
            whacdisps[3].text = "DO NOT WHACK";
            whacrends[10].enabled = false;
            Debug.Log(whacnames[whacforbid]);
        }
    }

    private IEnumerator WhacUp(int k)
    {
        float e = 0;
        Vector3 p = whacelements[k].localPosition;
        float[] ends = new float[2] { whacz[((k / 3) * 2) + 1], whacz[(k / 3) * 2] };
        while(game > 0 && pass == false && !irredeemablefailure)
        {
            yield return new WaitForSeconds(Random.Range(0f, 3.3f / speed));
            if (game < 1 || pass == true || irredeemablefailure)
                yield break;
            e = 0;
            int a = Random.Range(0, whaclimit);
            whacrends[k].material = whacmats[a];
            while(e < 0.1f)
            {
                e += Time.deltaTime * speed;
                whacelements[k].localPosition = new Vector3(p.x, p.y, Mathf.Lerp(ends[0], ends[1], e * 10));
                yield return null;
            }
            if (game < 1 || pass == true || irredeemablefailure)
                break;
            else
                whacelements[k].localPosition = new Vector3(p.x, p.y, ends[1]);
            whaccostume[k] = a;
            while(e < 3.9f)
            {
                if (whacpos[1] == k && whacswing)
                {
                    whacrends[k].material = whacmats[6];
                    break;
                }
                else if (pass == true || irredeemablefailure)
                {
                    whacelements[k].localPosition = new Vector3(p.x, p.y, ends[0]);
                    yield break;
                }
                e += Time.deltaTime * speed;
                yield return null;
            }
            e = 0.1f;
            if (game < 1 || pass == true || irredeemablefailure)
                break;
            whaccostume[k] = -1;
            while (e > 0)
            {
                e -= Time.deltaTime * speed;
                whacelements[k].localPosition = new Vector3(p.x, p.y, Mathf.Lerp(ends[0], ends[1], e * 10));
                yield return null;
            }
            whacelements[k].localPosition = new Vector3(p.x, p.y, ends[0]);
            yield return new WaitForSeconds(1);
        }
        if (pass == false)
            whacelements[k].localPosition = new Vector3(p.x, p.y, ends[0]);
        else
            whacrends[k].material = whacmats[7];
    }

    private IEnumerator WhacWin(int k)
    {
        Vector3 p = whacelements[k].localPosition;
        float[] ends = new float[2] { whacz[((k / 3) * 2) + 1], whacz[(k / 3) * 2] };
        float e = 0;
        while(game > 0)
        {
            e += Time.deltaTime * speed * Mathf.PI;
            whacelements[k].localPosition = new Vector3(p.x, p.y, Mathf.Lerp(ends[0], ends[1], Mathf.Abs(Mathf.Sin(e))));
            yield return null;
        }
    }

    private IEnumerator PlayerProximity(GameObject o, float radsq)
    {
        Transform player = bosselements[3].transform;
        Transform obj = o.transform;
        Vector3 scale = obj.localScale;
        bool lookleft = player.localPosition.x > obj.localPosition.x;
        while(game > 0 && o.activeSelf)
        {
            if (lookleft)
            {
                if(player.localPosition.x < obj.localPosition.x)
                {
                    lookleft = false;
                    obj.localScale = new Vector3(-scale.x, 1, scale.z);
                    if (o == bosselements[4])
                        bossfaceleft[1] = false;
                }
            }
            else
            {
                if (player.localPosition.x > obj.localPosition.x)
                {
                    lookleft = true;
                    obj.localScale = new Vector3(scale.x, 1, scale.z);
                    if (o == bosselements[4])
                        bossfaceleft[1] = true;
                }
            }
            float[] dist = new float[2] { player.localPosition.x - obj.localPosition.x, player.localPosition.z - obj.localPosition.z};
            if (!bossdamage[0] && (dist[0] * dist[0]) + (dist[1] * dist[1]) <= radsq)
                StartCoroutine("PlayerDamage");
            yield return null;
        }
    }

    private IEnumerator PlayerDamage()
    {
        bossdamage[0] = true;
        lives--;
        disptexts[3].text = (lives < 10 ? "0" : "") + lives.ToString();
        if (lives < 1)
        {
            game = 0;
            bossanim = 0;
            StopCoroutine("BossAttack");
            bosselements[3].SetActive(false);
            for (int i = 5; i < 35; i++)
                bosselements[i].SetActive(false);
            BGM.Stop();
            Audio.PlaySoundAtTransform("FailKidDeath", transform);
            yield return new WaitForSeconds(0.3f);
            bossrends[4].material = bossmats[54];
            Audio.PlaySoundAtTransform("FailUltimate", transform);
            bossrends[2].enabled = true;
            yield return new WaitForSeconds(5);
            bossrends[2].enabled = false;
            bosselements[35].SetActive(true);
            float e = 0;
            if (bossstability <= 190)
            {
                bossrends[31].material = bossmats[62];
                bossquote.text = "Combust deez nuts!";
            }
            else if (bossstability <= 260)
            {
                bossrends[31].material = bossmats[61];
                bossquote.text = "Looks like you drew the short fuse!";
            }
            else if (bossstability <= 330)
            {
                bossrends[31].material = bossmats[60];
                bossquote.text = "Your plans for beating me have\nblown up in your face!";
            }
            else
                bossquote.text = "You failed to make this bomb go boom.\nYour weak attempt has sealed your doom!";
            while(e < 0.5f)
            {
                e += Time.deltaTime;
                bosselements[35].transform.localEulerAngles = new Vector3(0, Mathf.Lerp(155, 175, e * 2), 0);
                yield return null;
            }
            float c = Mathf.Lerp(-3.481f, 3.481f, bossstability / 400f);
            e = 0;
            float d = 0;
            int f = 0;
            while(e < 3)
            {
                e += Time.deltaTime;
                d += Time.deltaTime;
                bosselements[36].transform.localPosition = new Vector3(Mathf.Lerp(3.481f, c, Mathf.Min(1, e)), 0.001f, 2.12f);
                if(d >= 0.1f)
                {
                    d = 0;
                    f ^= 1;
                    bossrends[30].material = bossmats[58 + f];
                }
                yield return null;
            }
            pass = false;
            screens[prompts.Length + 1].SetActive(false);
            StartCoroutine("Next");
            lives = 1;
        }
        else
        {
            Renderer k = bossrends[3];
            Audio.PlaySoundAtTransform("SFXKidDamage", bosselements[3].transform);
            for(int i = 0; i < 7; i++)
            {
                k.enabled ^= true;
                yield return new WaitForSeconds(0.2f);
            }
            for (int i = 0; i < 7; i++)
            {
                k.enabled ^= true;
                yield return new WaitForSeconds(0.1f);
            }
            bossdamage[0] = false;
        }
        yield return null;
    }

    private IEnumerator Traj(Transform obj, float a, float h, float c, float d, float t)
    {
        float e = 0;
        float y = 4 * obj.localScale.z;
        while(e < d)
        {
            e += Time.deltaTime / t;
            obj.localPosition = new Vector3( Mathf.Lerp(a, c, e), 0, Mathf.Lerp(2.45f - y, h, 4 * e * (1 - e)));
            yield return null;
        }
    }

    private IEnumerator Shockwave(GameObject wave, float x, float v, bool left)
    {
        Transform kid = bosselements[3].transform;
        wave.SetActive(true);
        wave.transform.localPosition = new Vector3(x, 0, 2.184f);
        wave.transform.localScale = new Vector3(left ? -0.15f : 0.15f, 1, 0.05f);
        while (left ? wave.transform.localPosition.x < 6.15f : wave.transform.localPosition.x > -6.15f)
        {
            wave.transform.localPosition += new Vector3((left ? 1 : -1) * v * Time.deltaTime, 0, 0);
            if (kid.localPosition.z >= 2.6f && Mathf.Abs(kid.localPosition.x - wave.transform.localPosition.x) < 0.55f && !bossdamage[0])
                StartCoroutine("PlayerDamage");
            yield return null;
        }
        wave.SetActive(false);
    }

    private IEnumerator ThrowBomb(int b)
    {
        GameObject bomb = bosselements[b + 5];
        float a = bosselements[4].transform.localPosition.x;
        float x = bossfaceleft[1] ? Random.Range(a, 4.7f) : Random.Range(-4.7f, a);
        float h = Random.Range(-4f, -2f);
        float e = 0;
        float d = 0;
        int f = 0;
        bossrends[b + 6].material = bossmats[41];
        bomb.SetActive(true);
        StartCoroutine(Traj(bomb.transform, a, h, x, 1, 1.5f));
        StartCoroutine(PlayerProximity(bomb, 0.35f));
        while (e < 1.5f)
        {
            e += Time.deltaTime;
            d += Time.deltaTime;
            if(d > 0.1f)
            {
                d = 0;
                f++;
                f %= 8;
                bossrends[b + 6].material = bossmats[f + 41];
            }
            if (!bomb.activeSelf)
                yield break;
            yield return null;
        }
        Audio.PlaySoundAtTransform("SFXBombSmall", bomb.transform);
        bomb.transform.localPosition = new Vector3(bomb.transform.localPosition.x, 0, 2.15f);
        bossrends[b + 6].material = bossmats[49];
        yield return new WaitForSeconds(0.2f);
        bomb.SetActive(false);
    }

    private IEnumerator LargeBomb(int b)
    {
        GameObject bomb = bosselements[b + 25];
        bossrends[b + 26].material = bossmats[52];
        float a = bosselements[4].transform.localPosition.x;
        float c = 0;
        float e = 0;
        bomb.SetActive(true);
        StartCoroutine(PlayerProximity(bomb, 0.6f));
        for (int i = 0; i < 3; i++)
        {
            c = bosselements[3].transform.localPosition.x;
            e = 0;
            StartCoroutine(Traj(bomb.transform, a, -3.6f, c, 1, 2.5f));
            while (e < 2.5f)
            {
                e += Time.deltaTime;
                bomb.transform.localEulerAngles += new Vector3(0, 1500 * Time.deltaTime, 0);
                if (!bomb.activeSelf)
                    yield break;
                yield return null;
            }
            Audio.PlaySoundAtTransform("SFXBombBounce", bomb.transform);
            a = bomb.transform.localPosition.x;
        }
        Audio.PlaySoundAtTransform("SFXBombLarge", bomb.transform);
        bomb.transform.localEulerAngles = new Vector3(0, 0, 0);
        bossrends[b + 26].material = bossmats[49];
        yield return new WaitForSeconds(0.5f);
        bomb.SetActive(false);
    }

    private IEnumerator BombFall(int b)
    {
        GameObject bomb = bosselements[b + 5];
        bomb.SetActive(true);
        bossrends[b + 6].material = bossmats[41];
        float x = Random.Range(-4.4f, 4.4f);
        bomb.transform.localPosition = new Vector3(x, 0, -4);
        StartCoroutine(PlayerProximity(bomb, 0.35f));
        float vel = 2f;
        Audio.PlaySoundAtTransform("SFXBombFall", bomb.transform);
        float d = 0;
        int f = 0;
        while (bomb.transform.localPosition.z < 2.15f)
        {
            vel += Time.deltaTime * 6;
            bomb.transform.localPosition += new Vector3(0, 0, vel * Time.deltaTime);
            d += Time.deltaTime;
            if (d > 0.1f)
            {
                d = 0;
                f++;
                f %= 8;
                bossrends[b + 6].material = bossmats[f + 41];
            }
            if (!bomb.activeSelf)
                yield break;
            yield return null;
        }
        Audio.PlaySoundAtTransform("SFXBombSmall", bomb.transform);
        bomb.transform.localPosition = new Vector3(x, 0, 2.15f);
        bossrends[b + 6].material = bossmats[49];
        yield return new WaitForSeconds(0.2f);
        bomb.SetActive(false);
    }

    private IEnumerator BombDrop(int c, float t)
    {
        for(int i = 0; i < c; i++)
        {
            if (game < 1)
                yield break;
            bossthrow[1]++;
            bossthrow[1] %= 20;
            StartCoroutine(BombFall(bossthrow[1]));
            yield return new WaitForSeconds(t / c);
        }
    }

    private IEnumerator BossAttack()
    {
        Transform kid = bosselements[3].transform;
        Transform boss = bosselements[4].transform;
        Renderer brend = bossrends[4];
        while (game > 0)
        {
            int attacklimit = Mathf.Min((((400 - bossstability) / 70) * 2) + 3, 8);
            do
            {
                bossattack += Random.Range(1, attacklimit);
                bossattack %= attacklimit;
            } while (bossattack == 6 && bosselements.Where((x, i) => i >= 25 && i <= 27 && x.activeSelf).Count() > 0);
            float e = 0;
            switch (bossattack)
            {
                default:
                    Audio.PlaySoundAtTransform("SFXRev", boss);
                    StartCoroutine(BossAnim(1));
                    yield return new WaitForSeconds(0.5f + (bossstability / 800f));
                    float acc = 0;
                    float vel = 6 + ((400 - bossstability) / 80);
                    acc = vel * 2;
                    if (!bossfaceleft[1])
                        vel *= -1;
                    bool accleft = false;
                    float d = 0;
                    accleft = bossfaceleft[1];
                    while (e < 8)
                    {
                        e += Time.deltaTime;
                        if (d > 0)
                            d -= Time.deltaTime;
                        else
                        {
                            if ((vel > 0 && boss.localPosition.x >= 3.8f) || (vel < 0 && boss.localPosition.x <= -3.8f))
                            {
                                d = Mathf.Abs(vel / 16);
                                if(Mathf.Abs(vel) > 6)
                                {
                                    Audio.PlaySoundAtTransform("SFXBombLarge", boss);
                                    StartCoroutine(BombDrop((int)Mathf.Abs(vel) - 5, 0.5f));
                                }
                                vel = 0;
                            }
                            if (accleft && vel < acc / 2)
                                vel += acc * Time.deltaTime;
                            else if (!accleft && vel > -acc / 2)
                                vel -= acc * Time.deltaTime;
                            if (accleft ^ bossfaceleft[1])
                            {
                                accleft ^= true;
                                Audio.PlaySoundAtTransform("SFXSkid", boss);
                            }
                            boss.localPosition += new Vector3(vel * Time.deltaTime, 0, 0);
                        }
                        yield return null;
                    }
                    Audio.PlaySoundAtTransform("SFXOutOfSteam", boss);
                    float cvel = 0;
                    cvel = vel;
                    e = 0;
                    while (e < 1)
                    {
                        e += Time.deltaTime;
                        vel = Mathf.Lerp(cvel, 0, e);
                        if (Mathf.Abs(boss.localPosition.x) < 3.8f)
                            boss.localPosition += new Vector3(vel * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    StartCoroutine(BossAnim(2));
                    yield return new WaitForSeconds(0.5f + (bossstability / 400f));
                    bossanim = 0;
                    break;
                case 1:
                    float t = 1 + (bossstability / 600f);
                    float a = boss.localPosition.x;
                    float c = a + ((kid.localPosition.x - a) * 2);
                    brend.material = bossmats[12];
                    Audio.PlaySoundAtTransform("SFXJump", boss);
                    StartCoroutine(Traj(boss, a, -3.5f, c, 0.45f, t));
                    yield return new WaitForSeconds(t * 0.45f);
                    brend.material = bossmats[13];
                    float drop = 0.2f + (bossstability / 2000f);
                    e = drop;
                    a = boss.localPosition.x;
                    Audio.PlaySoundAtTransform("SFXPound", boss);
                    while (e > 0)
                    {
                        e -= Time.deltaTime;
                        boss.localPosition = new Vector3(a, 0, Mathf.Lerp(2.4f, -3.5f, e / drop));
                        yield return null;
                    }
                    boss.localPosition = new Vector3(a, 0, 2.4f);
                    Audio.PlaySoundAtTransform("SFXBlast", boss);
                    for(int i = 0; i < 2; i++)
                    {
                        bossthrow[0]++;
                        bossthrow[0] %= 6;
                        StartCoroutine(Shockwave(bosselements[28 + bossthrow[0]], boss.localPosition.x, 8, i == 0));
                    }
                    yield return new WaitForSeconds(1 + (bossstability / 400));
                    boss.localPosition = new Vector3(a, 0, 2);
                    break;
                case 2:
                    Audio.PlaySoundAtTransform("SFXThrowWindup", boss);
                    StartCoroutine(BossAnim(3));
                    yield return new WaitForSeconds(0.5f + (bossstability / 400f));
                    float ammo = (15 + ((400 - bossstability) / 32)) * ((bossfaceleft[1] ? (3.55f - boss.localPosition.x) : (boss.localPosition.x + 3.55f)) / 7.1f);
                    for(int i = 0; i < ammo; i++)
                    {
                        bossthrow[1]++;
                        bossthrow[1] %= 20;
                        StartCoroutine(ThrowBomb(bossthrow[1]));
                        yield return new WaitForSeconds(5f / ammo);
                    }
                    yield return new WaitForSeconds(0.2f);
                    bossanim = 0;
                    brend.material = bossmats[7];
                    yield return new WaitForSeconds(0.8f + (bossstability / 400));
                    break;
                case 3:
                    a = boss.localPosition.x;
                    Audio.PlaySoundAtTransform("SFXFrontFlip", boss);
                    t = 0.5f + (bossstability / 800);
                    StartCoroutine(Traj(boss, a, -2.5f, a, 0.45f, t * 2));
                    StartCoroutine(BossAnim(4));
                    yield return new WaitForSeconds(t);
                    c = kid.localPosition.x;
                    if(Mathf.Abs(a - c) > 4)
                        c = bossfaceleft[1] ? a + 4 : a - 4;
                    e = 0;
                    float g = (1f + ((400 - bossstability) / 400f)) * 2;
                    while (e < 1)
                    {
                        e += Time.deltaTime * g * 0.75f;
                        boss.localPosition = new Vector3(Mathf.Lerp(a, c, e), 0, Mathf.Lerp(-2, 2, e));
                        yield return null;
                    }
                    float hspeed = Mathf.Clamp((c - a), -4, 4) * g * 0.5f;
                    float vspeed = 0;
                    int bounces = 4 + ((400 - bossstability) / 80);
                    StartCoroutine(BossAnim(5));
                    for(int i = bounces; i > 0; i--)
                    {
                        Audio.PlaySoundAtTransform("SFXBlast", boss);
                        vspeed = g * -1.25f;
                        while (vspeed < 0 || boss.localPosition.z < 2.1f)
                        {
                            if ((boss.localPosition.x > 3.7f && hspeed > 0) || (boss.localPosition.x < -3.7f && hspeed < 0))
                            {
                                Audio.PlaySoundAtTransform("SFXBlast", boss);
                                hspeed *= -1;
                            }
                            vspeed += Time.deltaTime * Mathf.Pow(g, 1.25f) * 2f;
                            boss.localPosition += new Vector3(hspeed, 0, vspeed * g) * Time.deltaTime;
                            yield return null;
                        }
                        hspeed *= 1 + (0.5f / bounces);
                    }
                    bossanim = 0;
                    brend.material = bossmats[7];
                    a = boss.localPosition.x;
                    boss.localPosition = new Vector3(a, 0, 2);
                    yield return new WaitForSeconds(0.5f + (bossstability / 800f));
                    break;
                case 4:
                    for (int i = 0; i < (500 - bossstability) / 100; i++)
                    {
                        brend.material = bossmats[26];
                        Audio.PlaySoundAtTransform("SFXWindup", boss);
                        yield return new WaitForSeconds(0.75f);
                        brend.material = bossmats[27];
                        boss.localPosition = new Vector3(bossfaceleft[1] ? 4 : -4, 0, 2);
                        if (kid.localPosition.z < 1.3f)
                            Audio.PlaySoundAtTransform("SFXWhiff", boss);
                        else
                        {
                            Audio.PlaySoundAtTransform("SFXExplosion", boss);
                            if(!bossdamage[0])
                                 StartCoroutine("PlayerDamage");
                        }
                        yield return new WaitForSeconds(0.5f + (bossstability / 800));
                    }
                    yield return new WaitForSeconds(0.75f);
                    break;
                case 5:
                    brend.material = bossmats[28];
                    Audio.PlaySoundAtTransform("SFXKickWindup", boss);
                    yield return new WaitForSeconds(1);
                    StartCoroutine(BossAnim(6));
                    Audio.PlaySoundAtTransform("SFXKick", boss);
                    bounces = 4 + ((300 - bossstability) / 60);
                    float speed = 6 + ((300 - bossstability) / 45f);
                    float[] v = new float[2] { bossfaceleft[1] ? speed : -speed, -speed};
                    for(int i = bounces; i > 0; i--)
                    {
                        while (v[1] < 0 || boss.localPosition.z < 2)
                        {
                            if ((boss.localPosition.x > 3.7f && v[0] > 0) || (boss.localPosition.x < -3.7f && v[0] < 0))
                            {
                                Audio.PlaySoundAtTransform("SFXBumper", boss);
                                v[0] *= -1;
                            }
                            if(v[1] < 0 && boss.localPosition.z < -4)
                            {
                                Audio.PlaySoundAtTransform("SFXBumper", boss);
                                v[1] *= -1;
                            }
                            boss.localPosition += new Vector3(v[0], 0, v[1]) * Time.deltaTime;
                            yield return null;
                        }
                        if (i > 1)
                        {
                            Audio.PlaySoundAtTransform("SFXBumper", boss);
                            v[1] *= -1;
                        }
                    }
                    Audio.PlaySoundAtTransform("SFXBlast", boss);
                    t = 1 + (bossstability / 300);
                    a = boss.localPosition.x;
                    StartCoroutine(Traj(boss, a, -3.5f, a, 0.5f, t));
                    yield return new WaitForSeconds(t / 2);
                    Audio.PlaySoundAtTransform("SFXTransform", boss);
                    bossanim = 0;
                    bossdamage[1] = true;
                    brend.material = bossmats[37];
                    yield return new WaitForSeconds(0.15f);
                    c = boss.localPosition.z;
                    e = 0;
                    t = 0.2f + (bossstability / 1000);
                    while(e < 1)
                    {
                        e += Time.deltaTime / t;
                        boss.localPosition = new Vector3(a, 0, Mathf.Lerp(c, 2, e));
                        yield return null;
                    }
                    Audio.PlaySoundAtTransform("SFXBombLarge", boss);
                    boss.localPosition = new Vector3(a, 0, 2);
                    for (int i = 0; i < 2; i++)
                    {
                        bossthrow[0]++;
                        bossthrow[0] %= 6;
                        StartCoroutine(Shockwave(bosselements[28 + bossthrow[0]], boss.localPosition.x, 12, i == 0));
                    }
                    yield return new WaitForSeconds(1 + (bossstability / 300));
                    Audio.PlaySoundAtTransform("SFXTransform", boss);
                    bossdamage[1] = false;
                    brend.material = bossmats[7];
                    yield return new WaitForSeconds(0.15f);
                    break;
                case 6:
                    Audio.PlaySoundAtTransform("SFXThrowWindupBig", boss);
                    for (int i = 0; i < Mathf.Max(1, (270 - bossstability) / 90); i++)
                    {
                        brend.material = bossmats[38];
                        yield return new WaitForSeconds(1);
                        Audio.PlaySoundAtTransform("SFXThrowBig", boss);
                        brend.material = bossmats[39];
                        bossthrow[2]++;
                        bossthrow[2] %= 3;
                        StartCoroutine(LargeBomb(bossthrow[2]));
                        yield return new WaitForSeconds(0.25f);
                    }
                    brend.material = bossmats[7];
                    yield return new WaitForSeconds(1.25f);
                    break;
                case 7:
                    GameObject bomb = bosselements[34];
                    Audio.PlaySoundAtTransform("SFXJumpSuper", boss);
                    brend.material = bossmats[0];
                    e = 0;
                    a = boss.localPosition.x;
                    while(e < 0.5f)
                    {
                        e += Time.deltaTime;
                        boss.localPosition = new Vector3(a, 0, Mathf.Lerp(2, -4, e * 2));
                        yield return null;
                    }
                    brend.enabled = false;
                    StartCoroutine(BombDrop(35 + ((190 - bossstability) / 19), 8f));
                    yield return new WaitForSeconds(1);
                    for (int i = 0; i < 3; i++)
                    {
                        yield return new WaitForSeconds(1);
                        a = Random.Range(-4.2f, 4.2f);
                        vel = 2;
                        bomb.SetActive(true);
                        bossrends[29].material = bossmats[53];
                        bomb.transform.localPosition = new Vector3(a, 0, -3.75f);
                        StartCoroutine(PlayerProximity(bomb, 0.5f));
                        while (bomb.transform.localPosition.z < 2)
                        {
                            vel += Time.deltaTime * 6;
                            bomb.transform.localPosition += new Vector3(0, 0, vel * Time.deltaTime);
                            if (!bomb.activeSelf)
                                yield break;
                            yield return null;
                        }
                        Audio.PlaySoundAtTransform("SFXBombLarge", bomb.transform);
                        bossrends[29].material = bossmats[49];
                        for (int j = 0; j < 2; j++)
                        {
                            bossthrow[0]++;
                            bossthrow[0] %= 6;
                            StartCoroutine(Shockwave(bosselements[28 + bossthrow[0]], a, 8, j == 0));
                        }
                        yield return new WaitForSeconds(0.35f);
                        bomb.SetActive(false);
                    }
                    a = kid.localPosition.x;
                    Audio.PlaySoundAtTransform("SFXWindup", boss);
                    yield return new WaitForSeconds(1);
                    brend.enabled = true;
                    brend.material = bossmats[40];
                    Audio.PlaySoundAtTransform("SFXExplosion", boss);
                    boss.localPosition = new Vector3(a, 0, 2);
                    if (!bossdamage[0] && Mathf.Abs(a - kid.localPosition.x) < 0.6f)
                        StartCoroutine("PlayerDamage");
                    for(int i = 0; i < 4; i++)
                    {
                        bossthrow[0]++;
                        bossthrow[0] %= 6;
                        StartCoroutine(Shockwave(bosselements[28 + bossthrow[0]], a, ((i / 2) * 8) + 4, i % 2 == 0));
                    }
                    yield return new WaitForSeconds(3 + (bossstability / 190));
                    break;
            }
        }
    }

    private IEnumerator BossAnim(int anim)
    {
        bossanim = anim;
        Renderer boss = bossrends[4];
        int f = 0;
        switch (anim)
        {
            default:
                while(bossanim == 1)
                {
                    boss.material = bossmats[f + 8];
                    f ^= 1;
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case 2:
                while (bossanim == 2)
                {
                    boss.material = bossmats[f + 10];
                    f ^= 1;
                    yield return new WaitForSeconds(0.35f);
                }
                break;
            case 3:
                while (bossanim == 3)
                {
                    boss.material = bossmats[f + 14];
                    f ^= 1;
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case 4:
                for (int i = 16; i < 24; i++)
                {
                    boss.material = bossmats[i];
                    yield return new WaitForSeconds(0.03125f * (1 + (bossstability / 400)));
                }
                break;
            case 5:
                while (bossanim == 5)
                {
                    boss.material = bossmats[f + 24];
                    f ^= 1;
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case 6:
                while (bossanim == 6)
                {
                    boss.material = bossmats[f + 29];
                    f += Random.Range(1, 8);
                    f %= 8;
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            case 7:
                for(int i = 55; i < 58; i++)
                {
                    boss.material = bossmats[i];
                    yield return new WaitForSeconds(1);
                }
                break;
        }
    }


    private IEnumerator Victory()
    {
        game = 0;
        StartCoroutine(BossAnim(7));
        StopCoroutine("BossAttack");
        bossfire.Pause();
        for (int i = 5; i < 35; i++)
            bosselements[i].SetActive(false);
        float e = 3;
        Vector3 p = bosselements[4].transform.localPosition;
        Vector3 s = bosselements[4].transform.localScale;
        Audio.PlaySoundAtTransform("PassUltimate", bosselements[4].transform);
        while (e > 0)
        {
            e -= Time.deltaTime;
            bosselements[4].transform.localPosition = new Vector3(p.x, 0, p.z) * (e / 3);
            bosselements[4].transform.localScale = s * (2 - (e / 3));
            yield return null;
        }
        BGM.Stop();
        bosselements[0].SetActive(false);
        bossrends[5].enabled = true;
        yield return null;
        buttons[2].AddInteractionPunch(5);
        StartCoroutine("Sparks");
        transform.Rotate(-7, Random.Range(-8, 8f), Random.Range(-9, 9f));
        smallscreens[2].SetActive(false);
        e = 0;
        int f = 51;
        float[] d = new float[2] { 0, Random.Range(0.1f, 0.3f) };
        BGM.loop = false;
        BGM.pitch = 1;
        BGM.clip = bgms[prompts.Length + 1];
        BGM.Play();
        while(e < 3)
        {
            e += Time.deltaTime;
            d[0] += Time.deltaTime;
            if(d[0] >= d[1])
            {
                d[1] = Random.Range(0.1f, 0.3f);
                d[0] = 0;
                f ^= 1;
                bossrends[5].material = bossmats[f];
            }
            if (Random.Range(e, 3) > 2.95f)
            {
                bossrends[5].enabled = false;
                BGM.mute = true;
            }
            else
            {
                bossrends[5].enabled = true;
                BGM.mute = false;
            }
            yield return null;
        }
        BGM.Stop();
        screens[prompts.Length + 1].SetActive(false);
        yield return new WaitForSeconds(3);
        pass = true;
        moduleSolved = true;
        StartCoroutine("Next");
    }

    private IEnumerator Sparks()
    {
        float e = 0;
        modsmash[1].Play();
        int c = 0;
        while (e < 10)
        {
            float d = Random.Range(0.5f, 2.5f);
            e += d;
            c = Random.Range(0, 4);
            sparkholder.localScale = new Vector3((2 * (c / 2)) - 1, 1, (2 * (c % 2)) - 1);
            modsmash[0].Emit(20);
            Audio.PlaySoundAtTransform("SFXSpark", sparkholder);
            yield return new WaitForSeconds(d);
        }
    }
}
