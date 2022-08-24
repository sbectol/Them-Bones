using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Twirly : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator[] catAnimator;
    private Animator middleCat;
    private GameObject cat;
    private GameObject holder;
    private GameObject outerTooth;
    private GameObject innerTooth;
    private GameObject innerToothHolder;
    private GameObject outerToothHolder;
    private GameObject catHolder;
    private GameObject longTooth;
    private GameObject longToothHolder;
    private GameObject molarTooth;
    private GameObject molarToothHolder;
    private GameObject middleCatHolder;
    public GameObject scrollView;
    private CanvasGroup showText;
    private CanvasGroup goBack;
    private TMP_Text messageText;

    private AudioClip audioClip;
    private AudioSource soundPlayer;

    private float rotationAmount = 4f;
    private Image button;
    private RectTransform buttonRectTransform;
    private AudioSource squeakPlayer;
    private AudioClip squeakClip;
    private float rotationSpeed;
    private float rate;
    private float buttonX;
    private float buttonY;
    private bool playTheSqueak;
    private int tempo = 96;
    private double bpm = 288.0F;
    private float friction = 0.3f;
    private readonly int speedLimit = 10;
    private float sliderValue;
    private bool running = false;

    private float gain = 0.2F;
    private int signatureHi = 0;
    private int signatureLo = 8;
    private string animationToPlay;
    
    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
  
    private Image disc;
    private GameObject discRect;
    private float r;
    public TMP_Text storyText;
    private string audioFile;

    void Start()
    {
        //beastName = PlayerPrefs.GetString("BeastName");
        scrollView.transform.localPosition = new Vector3(-2000, 0, 0);
        animationToPlay = PlayerPrefs.GetString("AnimationToPlay");
        soundPlayer = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        goBack = GameObject.Find("goBack").GetComponent<CanvasGroup>();
        showText = GameObject.Find("showText").GetComponent<CanvasGroup>();



        switch (animationToPlay) {
            case "1":
                storyText.text = "<size=120%><align=center>The Beast That Stalks Our Dream Time</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "Within the global network of modern science the animal and plant kingdoms are organized into an order in which everything has a name that makes it recognizable wherever you are in the world and whatever language you speak. Within this <i>taxonomy</i> these teeth, with their distinctive and rather chillingly serrated cutting edges, are identified as belonging to an extinct creature called <i>Homotherium latidens</i>. In English, this fearsome beast is known as the Scimitar-Toothed Cat.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Two centuries ago, whilst the Scimitar - Tooth still lurked on the edge of shadows, just beyond the bounds of the emerging scientific order, a footnote on a page within cave explorer Father John MacEnery’s book <i>Cavern Researches</i> described the beast thus:";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>In this island anterior to the deposition of the drift, there was associated with the great extinct Tiger, Bear and Hyaena of the caves, in the destructive task of controlling the numbers of the richly developed order of the herbivorous Mammalia a feline animal as large as the Tiger, and, to judge by its implements of destruction, of greater ferocity...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In this extinct animal, as in the Machairodus cultridens of the Val d’Arno and the Machairodus megantereon of the Auvergne, the canines curved backwards, in form like a pruning knife having the greater part of the compressed crown provided with a double-cutting edge of the serrated enamel...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Thus, as in the Megalosaurus, each movement of the jaw with a tooth thus formed combined the power of the knife and saw; whilst the apex, in the making of the first incision, acted like the two-edged point of a sabre. The backward curvature of the full grown teeth enabled them to retain, like barbs, the prey whose quivering flesh they penetrated.</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "An indisputably visceral vision...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "But what would we make of these fangs if we were transported even further back in time; beyond the dawning of the age of reason to a world without internet, without a global network of researchers, without reference books and photos? What if we could only <i>imagine</i> what beast these six inch canines might have belonged to? Native Americans, for example, told (and still tell) vivid tales stemming from the dinosaur bones they encountered in the Badlands, weaving them into their seasonal mythologies such that they became timelessly relevant.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "What would these teeth mean in a world of stories?";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "And what would it be like to live in a world where knowledge was shared and passed down through the generations by such stories? Where what was known and <i>true</i> was bound up in myths and legends that were vivid and memorable – but weren’t written down because people chose to remember in non - literary ways.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "What would happen to these stories over thousands of years as they were told again and again, passing through many minds?";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "These are important questions because in some places this is still the way and modern science must find ways to co-exist with something much older, enduring – and very deeply human.";
                audioFile = "Homotherium 1";
                break;
            case "2":
                storyText.text = "<size=120%><align=center>The Deluge</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "Ten thousand years ago in the Near East – but at other times elsewhere – people learned to grow crops and corral herd animals. This meant they could stay in one location and didn’t have to carry everything around with them, so perhaps were better placed to create things from materials that were heavier and more permanent in form. As technologies evolved they began to write things down in more enduring formats so as to ‘fix’ them; literally setting them in stone.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Perhaps they thought – as we might – that this would make things clearer and less prone to dispute, although emails often demonstrate that this isn’t always the case. On the whole, they compiled lists that showed fairly boring but nonetheless important stuff relating to stocks, crops, ownership and that sort of thing. But they did occasionally write down stories as well – and one tablet of what is known as cuneiform writing has been translated as relating the familiar story of the Great Flood. It’s called the Ark Tablet and it was created nearly two thousand years before the time of Jesus.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Perhaps, as this time of a written truth emerged, the universe of memory held in myth and legend became increasingly submerged, along with the memory of great beasts once hunted and now banished to the edge of shadows. But the mortal remains of these creatures resurfaced occasionally to set the imagination spinning – particularly when they evoked sharp - toothed predators that might have feasted on us.And apparently this powerful magic remains today...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In some places, the memory held in stories stayed strong and is now being shown to tally with scientific research. This is true of Aboriginal flood stories in Australia which are thousands of years old. Such tales, in which many generations of knowledge are bound up, have been preserved because what they say is, for one reason or another, of great importance to those people. In Arnhem Land living stories of the Thylacine – the marsupial wolf – are still told two thousand years after its disappearance from that landscape. Maintaining the memory of the Thylacine as a component in the universal order is important to the people who live there because they recognize themselves as being part of that order. The Thylacine is part of who they are – as is its story.";
                audioFile = "Homotherium 2";
                break;
            case "3":
                storyText.text = "<size=120%><align=center>Catcott and Slutch</size>\n(And The Mis - identified Moofe - Deer)</align>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "We now know that around 12,000 years ago the climate was warming, causing the ice sheets that had covered much of the landmass familiar to us as the British Isles and Ireland to gradually melt. Year by year, decade by decade, century by century the map changed – as it always does. Beneath the ground in caves the bones of wild beasts were slowly covered by layers of earth that built up inch by inch as time passed – just as the beasts themselves became buried deeper and deeper in memory.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In the eighteenth century, natural scientists – often men of the Church who were the only ones with the time to think about such things and commit their thoughts to paper – began to unearth these bones in the course of their philosophising. This created a number of significant puzzles because, rather confusingly, many of the bones were huge and the animals from which they might have come were nowhere to be seen in the locale. And we have to remember that at this time extinction was not known, let alone understood. And also that The Truth was by and large owned by the Church.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Therefore, it was only natural to conclude that the answer to the conundrum of the missing beasts lay in the Old Testament. These bones had been washed into caves by Noah’s Flood; they were victims of The Deluge.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "The Reverend Alexander Catcott’s A Treatise on the Deluge (1768) carefully lays out the rationale for this and includes a detailed and carefully considered deck plan for the Ark, showing how all the species were quartered and where the considerable food supplies necessary to keep them from eating each other were stored.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Catcott also muses on absent species – and the vexing presence of giant antlers in ‘the Slutch’ of the Deluge in Ireland (deposits which we now attribute to glaciation). These, he concludes, must belong to the ‘moofe-deer’ – the massive ‘Mus’ (or moose) encountered by explorers in the New World.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "However, in the fullness of time, science has shown that these incredible racks were the crowning glory of the Giant Deer Megaloceros giganteus, the extinct cervid formerly known as the Irish Elk.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "But for Catcott – and indeed wider society – The Word, The Truth was God.";
                audioFile = "Homotherium 3";
                break;
            case "4":
                storyText.text = "<size=120%><align=center>The Conflicting Universes of the Rev. Dr. Buckland</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "Fifty years later, ‘the Deluge’ was still seen as the principal agency by which the remains of elephants and rhinoceroses had come to rest in the caves of Wales and England. But, wondered the Rev. Dr. Buckland of Oxford University, HOW were such vast carcasses washed through what were often narrow cave entrances? His explorations of Kirkdale Cave in Yorkshire led him to the answer. Here, the cave floor was carpeted by bones – and many small round white balls. These, he came to realise, were the fossilised dung balls of hyaenas. So the bones had been carried into the cave by hyaenas. Kirkdale Cave was, before the Flood, a hyaena den.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "To prove this, Buckland acquired a hyaena and fed it great joints of beef, closely observing its actions at both ends. At the anterior end its incredibly powerful jaws imparted bite marks identical to those on bones he had found at Kirkdale. And at the posterior end, white balls of ‘album graecum’ (ie. poo full of bone) popped out. These he called ‘coprolites’ from the Greek kopros (meaning dung) and lithos (meaning stone).";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "He published his findings in Reliquae Diluvianae or ‘Relics of the Flood’ in 1823 having concluded that the Deluge had occurred as per the Old Testament, that hyaenas, elephants and more had inhabited the Yorkshire landscape prior to its engulfment – and that Humankind had arrived there after the Flood.Thus, a clear separation between beast and the rational mind of man entirely fit for this Age of Reason.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Reliquae Diluvianae is a beautiful book, whose hand typesetting and engraved illustrations carry great gravitas. It is a work of authority; a testament. Yet in wider society there was considerable amusement concerning Buckland’s fixation with the hyaena. But his method of excavating Kirkdale Cave and subsequent findings were nevertheless an important advance – and caused the first cracks in the Flood-story-as-truth to appear.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Buckland had deduced that as he dug further and further down through layers of soil in the ‘cave earth’ he was effectively travelling back through time. And as he did so the idea that the Creation had taken place at nightfall on 22nd October 4004BC, as Bishop Ussher had calculated, seemed increasingly untenable.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "However, in the fullness of time, science has shown that these incredible racks were the crowning glory of the Giant Deer Megaloceros giganteus, the extinct cervid formerly known as the Irish Elk.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "This, for a man of The Church, must have been deeply troubling...";
                audioFile = "Homotherium 4";
                break;
            case "5":
                storyText.text = "<size=120%><align=center>William Beard of Bone Cottage, Banwell\n</size>(and his Bear)</align>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "In 1824, in search of new sources of income for the village, the vicar of Banwell in Somerset commissioned two miners to reopen a cave system that had been first discovered in 1757.The new Bishop of Bath and Wells George Henry Law who owned the land was delighted at what they found; a mass of animal bones including bear, reindeer, bison and wolverine.This, in the light of discoveries made elsewhere, presented solid proof of the Deluge, the catastrophic flood unleashed by God in order to cleanse the Earth of humanity’s wickedness. So, compelling evidence of the need for the Church to exert moral authority for the well-being of all.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Over the ensuing three decades, William Beard (of Bone Cottage, Banwell) acted as curator of Banwell Bone Cave, leading guided tours for which he extracted a charge from visitors. His diary, held in the Somerset County Archives, demonstrates not only that (somewhat unsurprisingly) he got through a whole lot of candles – but also that he welcomed an extraordinary array of people to the caves including local laymen, illustrious cave researchers including William Buckland and William Pengelly and countless representatives of what might be thought of as ‘the chattering classes’ of the period.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "This last group in particular demonstrates the powerful grip that this unfurling narrative – concerning the expansion of time itself, the making of the Earth and the origins of humanity – exerted over society. What was being debated over the decades of Beard’s dominion at Banwell was perhaps equivalent in terms of impact to what the discovery of life on another planet would be for us today. Or perhaps the reality of anthropogenic climate change, which has taken a similar timespan to become accepted in all rational quarters...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "It was, it seems, a battleground over which the established Authority fought to retain control in the face of a whole new universal Truth. There was, of course, no television or internet at the time so on the whole neither news nor people travelled as quickly or easily as they do today. And the latter is testament to the powerful allure of what was being presented by Beard at Banwell; a great many people were prepared to travel considerable distances to gaze on the subterranean marvels of which he was custodian (albeit on behalf of the Bishop).";
                audioFile = "Homotherium 5";
                break;
            case "6":
                storyText.text = "<size=120%><align=center>The Silencing of Father John MacEnery</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "In order to advance his understanding of the great natural forces at play in the world, William Buckland traversed back and forth across England and Wales at great pace. In 1823 he wrote to Lady Mary Cole of the wealthy Talbot family of Penrice Castle, Gower:";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>Since I left you I found they have discovered two large Tusks of Tygers at Plymouth and passing through Wells I heard tidings of human bones at Wokey(sic) Hole...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Leaving Wells in a chaise with a strange Gentleman he told me of another Cave with Bones near Axbridge, of which I did not have time to stop to make further enquiries as I was nearly buried in snow.I have just got news of more caves at Gibraltar off in Spain...</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Scientific disciplines as we would recognise them were now slowly emerging. Amidst this evolution, Buckland, reflecting wider society, maintained his position that the Biblical Flood had occurred – effectively wiping out the great beasts that had once inhabited the British landscape – and that humanity had emerged after the waters had subsided. Thus, elephants, hippopotami, rhinoceroses and their ilk had not co-existed with people.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "However, in Devon at Kent’s Hole in Torquay – known today as Kents Cavern – Father John MacEnery was making discoveries that brought this into question. For beneath a hard layer in the cave earth of what is called calcite, amidst the bones of extinct animals, he found flint tools that were indisputably made by people. This indicated that humans and beasts had once shared the West Country landscape, calling Buckland’s conclusion into question.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Buckland, however, was the eminent academic; the Man of Oxford. And so the idea of these implements being made by humans before the Flood was dismissed and, correspondingly, the integrity of MacEnery’s findings and methods called into question.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "MacEnery had made other finds of resonance which bring to mind the ‘Tusks of Tygers’ referred to by Buckland in his letter to Lady Mary. Within a part of the cave system he referred to as the Wolf’s Den MacEnery unearthed five spectacular canine teeth and an incisor. These he passed onto Buckland – who was unable to identify them. Eventually they were recognised by Baron Cuvier in Paris as being identical to those of a creature he’d pronounced to be a new species of bear Ursus cultridens that had been discovered in the Val d’Arno, Italy.Reflecting the rapid evolution of the scientific understanding of the time, the ‘bear’, however, was very soon to become a cat...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Taxonomical issues aside, as noted by MacEnery’s fellow West Country cave explorer William Pengelly (more on whom later), other researchers – including the eminent Dr Hugh Falconer – dismissed the possibility of MacEnery having found the teeth in Kent’s Hole. Falconer, a geologist, botanist, palaeoecologist and palaeoanthropologist suggested that MacEnery must have obtained the teeth in Italy and then incorrectly – though ‘in good faith’ – ascribed them to his favourite cavern.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Nevertheless, in the absence of photography which was yet to become available as a means of documenting specimens, a lithograph depicting the teeth was created by Buckland’s rather less well known wife Mary – also a geologist and a scientific illustrator of considerable talent. (Rev. Buckland, appears to have disapproved of women publicly engaging in scientific pursuits so she has largely remained in the shadows...)";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "This beautiful plate, commissioned by MacEnery at his own expense and ascribing the teeth to Ursus cultridens, was to be included in his own monograph Cavern Researches; the definitive account of his great labour of love at Kent’s Hole. Buckland, however, appears to have sown sufficient seeds of doubt in MacEnery’s mind concerning the evidence he saw before him – and the book would never be published in his lifetime.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "For whatever reason, it appears that a cloud was being cast over his work by Establishment figures.In 1829, whilst working in his beloved Kent’s Hole, MacEnery was overcome by foul air and never fully recovered.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Pengelly later observed that MacEnery";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>‘seems to have been fully aware that his discovery was important and, so far as Britain was concerned, unique... it may be supposed, perhaps, that he anticipated scepticism, and lost no! opportunity for furnishing the evidence which it demanded.’</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Amidst this rather toxic fug, the only certainty would seem to be that the spectacular teeth from the Wolf’s Den, with their saw-like cutting edges, had cast a spell over these men of the new science...";
                audioFile = "Homotherium 6";
                break;
            case "7":
                storyText.text = "<size=120%><align=center>Big Beasts Stalk the Mind of Sir William Boyd Dawkins</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "In 1857, a thrusting young man with extraordinary energy went up to Jesus College, Oxford. His name was William Boyd Dawkins, the son of a vicar, and it was during his time at the university that the Great Question surrounding what was referred to as the ‘Antiquity of Man’ was answered. To determine this, as the sciences of archaeology, geology and palaeontology became more distinct as disciplines, it had become increasingly acknowledged that rigorous methods must be adopted in order to avoid the doubt that had been cast upon MacEnery’s discoveries.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Accordingly, when a new and undisturbed cave was found at Windmill Hill, near Brixham in Devon, a Committee was formed; of eminent men including Hugh Falconer, Charles Lyell (the author of Principles of Geology), Richard Owen (who devised the word ‘dinosaur’) and William Pengelly of Torquay. Using a method devised by Pengelly, the cave was carefully excavated – and stone tools found beneath the sealed calcite layer alongside the bones of extinct cold climate creatures. Humans must, therefore, have lived in Devon in ancient times – and at the same time as the woolly beasts.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Whilst this in itself was still not enough to convince the scientific world, when eventually considered alongside other evidence from sites in France, the doubts were finally dispelled and in 1859 it became accepted that humans had been present for much longer than the 6,000 years of Bishop Usher’s reckoning. And that they had, at times, co-existed in a constantly changing environment with exotic creatures who no longer roamed the landscapes of Wales and England. And therefore, finally, that all this had nothing to do with Noah’s Flood.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Also in 1859, Charles Darwin published On the Origin of Species. What an extraordinary and dizzying time this must have been – with news stories constantly breaking that changed all understanding of both the origins of humanity and planet Earth itself. Amidst all these great men of the new sciences, the youthfully vigorous and highly driven William Boyd Dawkins was determined to forge a reputation and thereby become a part of this pantheon himself.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In December of that year he duly began the excavation of the Hyaena Den at Wookey Hole in Somerset. Here, stationing himself at the entrance of the cave he examined buckets of cave earth extracted by the local workers who toiled within. He records the bones of many large mammals in quick scribbles in his notebook; a sharp contrast to the methods employed by Pengelly at Windmill Hill earlier that year. And perhaps to his great satisfaction, he too discovered human artefacts – though whether, given the excavation method, his findings would bear the same intense scrutiny as those of Pengelly is debatable.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Dawkins, like his fellow ‘Cave Hunters’ appears to have been drawn to big beasts; the mammoth, giant deer, woolly rhinoceros, cave lion and hyaena. Given the subject matter of cave paintings and the enduring popularity of modern safari parks, maybe this is true of us all...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "We’ve seen already the importance of the written word and the Big Book in establishing Truth. In 1866, Dawkins, in partnership with William Ayshford Sanford, embarked on the assembly of a massive work; The British Pleistocene Mammalia. This beautifully illustrated and comprehensive compendium would, Dawkins intended, be the definitive treatise on all of the discoveries made in the previous decades. It would, perhaps, be his monument – and was an idea conceived whilst Dawkins was still in his twenties.";
                audioFile = "Homotherium 7";
                break;
            case "8":
                storyText.text = "<size=120%><align=center>William Pengelly and the Cavern of Truth</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "William Pengelly was a Cornishman, the son of a sea captain who left school at the age of twelve to join his father’s crew. He was shipwrecked once and saved from drowning twice before being brought back to Looe on the death of his younger brother in an accident. Here, he set about teaching himself mathematics before becoming a private tutor.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Perhaps these early experiences gave him a different perspective to the Oxford Men, Buckland, Lyell, Dawkins and all. And maybe it was this that enabled him to navigate the distinctly choppy political waters surrounding the excavation at Windmill Hill Cave, Brixham, which was to play such a significant role in establishing the so-called ‘Antiquity of Man’. Correspondence suggests the Committee to be a wriggling mass of egos and untruths as the men of science each sought to claim credit for the important discoveries being made. In all this subterfuge, the name of Falconer – who had cast doubt on the provenance of MacEnery’s ‘bear’ teeth – particularly stands out.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "The teeth themselves had, over the course of three decades, been the subject of a debate almost as fierce as the predator from whose formidable dentition they hailed. In this time, it had been reclassified, ceasing to be a bear (Ursus cultridens) and becoming Machairodus latidens; something like a cat. Indeed, as early as 1826 Buckland had written of them as being from";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>‘an unknown carnivorous animal, at least as large as a tiger; the genus of which has not yet ! been determined.’</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Pengelly, like MacEnery before him, seems to have been deeply wedded to Kents Cavern – possibly a reflection of his West Country roots. Perhaps because of this, he seems to have supported MacEnery where Buckland, Falconer and the others had cast doubt on his work – which, we should remember, had yielded solid evidence of the ‘Antiquity of Man’ four decades before the excavation at Windmill Hill. And within the story of the ‘Cave Hunters’, as these Victorian gentlemen became known, the Machairodus of Kent’s Hole seems to have become the conflicted avatar of truth – or untruth – in carnivorous, nearly feline, form.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Following in MacEnery’s footsteps, Pengelly set about the excavation of Kents Cavern in 1864 using the same meticulous techniques that he had evolved for Windmill Hill Cave. He later wrote;";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>‘in 1858, the results of the systematic and careful exploration of Brixham Cavern, on the opposite shore of Torbay, induced the scientific world to suspect that the alleged discoveries which, from time to time during a quarter of a century, had been reported from Kent’s Hole, might, after all, be entitled to a place amongst the verities of science’.</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "But along with the ‘verities of science’ the Machairodus also appears to have occupied his thoughts:";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In his excavation report for the earlier part of 1871 he laments;";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>‘The Branch of the Cavern termed the Wolf’s Den by MacEnery, and in which he found the celebrated five fine canines of Machairodus is immediately on the right, or north, of our present! work but we still have to state that we have met with no trace of that huge Cave mammal.’</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "And then later that year;";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>We commenced the exploration of the Wolf’s Den on July the 12th 1871, and from time to time ! we cherished hope that we might, like MacEnery, find in it some traces of Machairodus.</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "But on the 29th July, 1872, after eight seasons of digging Pengelly finally – yet with no apparent rush of emotion – records;";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>No. 5962. In Cave-Earth, 4th parallel, 1st Level, 1 yard right, including 1 tooth in left lower jaw of Bear and 1 of Machairodus.</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "At last! What a moment when the candle illuminated the distinctive serrated edge after eight years of painstaking digging. And what a contrast in surveying and recording methods to Dawkins’ hastily scribbled notes from the Hyaena Den...";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Pengelly’s work at Kent’s Cavern was to continue with the same rigour and mathematical detail for a further eight years. By the time of its conclusion MacEnery’s work had been thoroughly vindicated – nearly forty years after his death.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "And like William Beard at Banwell earlier in the century, Pengelly records a steady trickle of dignitaries – amongst them the nobility of Europe including members of the British, Dutch and Russian royal families and even Napoleon III – making pilgrimages to Torquay to inspect the discoveries that were profoundly altering the Victorian universal view.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Plainly, the work of the Cave Hunters still held a grip over society... and the scimitar-toothed one still had them in its clasp...";
                audioFile = "Homotherium 8";
                break;
            case "9":
                storyText.text = "<size=120%><align=center>The (Alleged) Fall of Sir William Boyd Dawkins</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "In 1873, Mr Frank Tebbet, supervisor at the Creswell Crags limestone quarry in Nottinghamshire found fossils in the mouth of Robin Hood Cave, one of a series of caves in the rock face of the spectacular gorge. As a result, in July 1875 the Reverend John Magens Mello, a graduate of St. John’s College, Oxford and Thomas Heath, the curator of Derby Museum and Art Gallery commenced excavations in the gorge. Heath, the son of a tile cutter, had left school aged twelve to take up an apprenticeship in the same profession as his father but abandoning it, had progressed to assume the curatorship of the museum by the time he was twenty five.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "They quickly found not only the bones of horse, woolly rhinoceros, bison, hyaena, mammoth, elk and lion – but also stone tools.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "By the following summer another familiar Oxford Man, William Boyd Dawkins, now curator of the Manchester Museum and a professor, had manoeuvred himself into the position of site palaeontologist. Along with Mello and Heath, Dawkins formed part of the Exploration Committee which was made up of preeminent (and entirely male) researchers in the field. As at Windmill Hill Cave, the committee appears to have been a hotbed of academic rivalries – and there was already considerable animosity between Dawkins and Heath. The excavations were not, however, conducted with the mathematical precision of Pengelly’s in Devon – perhaps, in fairness, because of the cost of doing so.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "The 3rd of July 1876 saw a momentous discovery which would have far-reaching consequences. Heath recorded that ten minutes after Dawkins appeared at Robin Hood Cave for the first time that day;";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>‘when Mr Dawkins was chatting with my friend and me, the men laid bare a small escarpment, when we saw a canine, which Mr Dawkins immediately recognised and exclaimed “Hurrah! The! Machairodus!”</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Heath’s later testimony, also attributed to the workman Moses Hartley, extends Dawkins’ exclamation adding;";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "<indent=5%><i>“Oh my! Pengelly will go wild when he hears of this! It will spread like wildfire over Europe!”</i></indent>";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "The beast had resurfaced once again.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "The tooth, of Homotherium latidens, the Scimitar-toothed Cat was to provide the spark that ignited a very public and embittered war of words between Dawkins and Heath. For Heath harboured suspicions that Dawkins had planted it in the cave.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "After remaining silent for three years, he called Dawkins out.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "We can never know for sure the precise truth, which resides in two rival testimonies. But what is certain is that Dawkins proceeded to use the media of the day, his influence and his status to destroy Thomas Heath’s reputation. The battle between the two effectively became a soap opera played out in the national and international media.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Dawkins, it has been suggested, had sought to engineer a story to rival Pengelly’s at Kent’s Cavern, effectively tampering with the science. And in doing so, he cast a shadow over his own reputation – which remains tarnished to this day.";
                audioFile = "Homotherium 9";
                break;
            case "10":
                storyText.text = "<size=120%><align=center>Sir William Boyd Dawkins: Laying the Ghost at Victory Quarry</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "The controversy of the ‘Creswell Incident’ was – for whatever reason – rooted in Dawkins’ apparent capacity for manipulating the truth in order to achieve his own ends. This single episode has by and large clouded his extraordinary achievements as a palaeontologist and geologist.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "By contrast, Pengelly’s measured, analytical and more selfless approach has seen researchers beat a path to Torquay Museum (of which he was a founder) right up to the present day to pore over finds from Kents Cavern. Science demands rigour and precision.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In 1881, shortly after the saga of the Homotherium tooth and his annihilation of Thomas Heath, Dawkins gave up almost all palaeontological and archaeological research. It wasn’t until 1902 that he undertook what would be his final excavation at Victory Quarry, near Doveholes in Derbyshire. Here, a local boy ‘Master Hicks’ had brought some bone fragments to the Buxton antiquary Mr Micah Salt – who drew Dawkins attention to the site. Amongst the fossils were the teeth and leg bones of the scimitar-toothed cat Homotherium crenatidens – the predecessor of the beast found at Kent’s Cavern and Creswell Crags.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "It would be easy, in the pursuit of a good story, to cast William Boyd Dawkins as the pantomime villain. But the scope of his work with Pleistocene fossils is staggering and to consign him thus would be to dismiss his strong advocacy for museums as a force for public edification. He mentored J. Wilfred Jackson who left school aged twelve but became a Doctor of Science and curator at Manchester Museum and Herbert Balch, who entered the Post Office to work aged fourteen and eventually founded the Wells and Mendip Museum. Both dedicate monographs to Dawkins – and it is clear that he was a positive force in sparking the passion that drove them both to important endeavours in their respective landscapes. Beyond the museum world he was a fighter for workers rights – especially in the coal mining industry – lobbying hard to get a better education system for miners similar to the ones established in Germany.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Perhaps the man who so relentlessly pursued big beasts had one within – aroused by rivals who sparked in him what seems an irrepressible need to be the Alpha. He was not alone in this. Like stags in autumn these nineteenth century men of science roared, rushed at each other and locked antlers, seeking to gain advantage over their opponents at any cost – including, at times, the truth. And in doing so, they undermined the integrity upon which science – and indeed all society – relies.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "But perhaps in the end Dawkins’ inner beast was tamed – by the scimitar-toothed one who, in a new century, drew him back into the field one last time in order to exorcise the demons of past wrongs.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Dare we forgive him?";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Can we afford to?";
                audioFile = "Homotherium 10";
                break;
            case "11":
                storyText.text = "<size=120%><align=center>The Quiet Resolve of Dorothea Bate</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "In 1898, four years before William Boyd Dawkins’ final encounter with the Homotherium at Victory Quarry, a young woman presented herself for work in the Bird Room at the Natural History Museum in London. Dorothea Bate was largely self-taught, observing later that her education was ‘only briefly interrupted by school’. At this time women weren’t employed as scientists so Bate was taken on – based on her exceptional skill – as an ‘unofficial scientific worker’, paid by the number of specimens she prepared.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Growing up in the countryside of Carmarthenshire had imbued her with an insatiable passion for exploring her natural surroundings and it was this, combined with her quiet but formidable determination that was to propel her pioneering career as a scientist.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "In the same year as Dorothea’s now legendary entrance to the Natural History Museum, the Bate family moved from west Wales to the Wye valley. Now venturing into the border countryside, she explored the limestone caves of the river gorge where, having scrambled up a particularly precipitous scree slope, she eventually reached Merlin’s Cave. Here, in the darkness, she found embedded in the walls the fossil bones of an array of small mammals including lemming and the pika or tailless hare. Now gone from the British landscape but still extant in Northern Europe and Siberia, the presence in caves here of the remains of these cold-climate species yields information no less valuable than the more immediately striking ‘trophy’ remains of megafauna that were so prized by the gentleman Cave Hunters of the nineteenth century.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "She was encouraged by her new colleagues at the Natural History Museum to write up her discoveries which were presented in the Geological Magazine as A Short Account of a Bone Cave in the Carboniferous Limestone of the Wye Valley. This was to be the first of many published papers in a distinguished career that continued right up to her death in 1951.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "She is remembered with respect and great affection as ‘the spark that would ignite a project’. Perhaps her use of dynamite as a collection tool was more Dawkins than Pengelly – but her selfless demeanour, along with her willingness to cast her research net over creatures small as well as great, sets her apart from the overwhelming majority of her ‘cave hunting’ predecessors. A towering – yet unassuming – figure in twentieth century palaeontology, she contributed much to our understanding of the impacts of climate and environmental change on mammalian species over the millennia.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Undertaking fieldwork not just in Britain but also the Mediterranean and Africa, Dorothea Bate was a significant but quietly rotating cog in what had become a highly complex, inter-connected and globalised science machine.";
                audioFile = "Homotherium 11";
                break;
            case "12":
                storyText.text = "<size=120%><align=center>The Cave In The Mind of Professor Schreve</align></size>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "Danielle Schreve is today, Professor of Quaternary Science in the Department of Geography at Royal Holloway, University of London. The Quaternary Period is the last 2.6 million years and is divided into two epochs; the Pleistocene (2.58 million years ago to 11.7 thousand years ago) and the Holocene (11.7 million years ago to the present day).";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Using a variety of processes available to modern science, she extracts information from ancient animal bones and interprets the data so as to understand the impacts of abrupt climate and environmental change on the fauna of the period. In this way, she can help to predict what the impacts of future climate change may be because many of these creatures – or at least their close relatives – are still with us. And thus we can gain an idea of what may happen to entire ecosystems and therefore – because we are a part of them – to us.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Her research draws and builds upon the scientific legacy of Dorothea Bate – and indeed the Cave Hunters of the nineteenth century going all the way back to William Buckland, who collectively deduced that our landscape had been inhabited by both cold and warm climate creatures at different times over many millennia. Science isn’t about one person or one fixed story. It’s a constantly unfurling and morphing narrative of twists and turns – which sometimes springs surprises as new discoveries are made.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Danielle has been excavating Gully Cave in the Mendip Hills in Somerset since 2006. Each year she and a team undertake a couple of weeks of meticulously planned digging using methods which both Buckland and Pengelly would recognise and endorse – though perhaps not Dawkins. Soil is removed and sieved with painstaking care, for as Dorothea Bate showed at Merlin’s Cave, tiny mammal bones have at least as much of a story to tell as those of the megafauna. Material is finally removed to the lab at Royal Holloway where analysis is undertaken for the rest of the year and beyond – bringing to bear new technologies that would astonish the antiquarians such as ancient DNA analysis, 3D visualisation and bone chemistry to reconstruct past diet and movements.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "This research is steadily forming a high resolution picture of the changes that have taken place over the last 100,000 years; how species have come and gone as the climate has warmed and cooled. The reindeer for example, was present 11,500 years ago but no longer runs over the Mendip Hills because it’s now too warm there for a creature so perfectly adapted to the cold. They, as we know well, are solely creatures of the Arctic and Sub-Arctic – but how long before it becomes too warm for them there? When this comes to pass they will vanish. And perhaps we, who are dependent on healthy biodiversity for our own well-being, will follow, having first been forced to retreat to habitable refugia – as science shows our ancestors did before us.";
                audioFile = "Homotherium 12";
                break;
            case "13":
                storyText.text = "<size=120%><align=center>Udfil Rhyfeddol y Doethur Jones\n</size>(Dr Jones’ Astonishing Hyaena)</align>";
                storyText.text += "\n"; storyText.text += "\n";
                storyText.text += "Dr. Angharad Jones, curator at Creswell Crags Museum, has a PhD in the study of fossil hyaena evolution and behaviour and is part of the global research network. To achieve this distinction she studied under the tutelage of Danielle Schreve at Royal Holloway University, beginning by devising a scientific question that hadn’t been asked before, then attempting to answer it – and finally having her findings and conclusion scrutinised and then questioned by a group of experts in the field.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "This, essentially, is what happens in all science, where every piece of research is ‘peer reviewed’ – meaning that other expert researchers with relevant knowledge and experience consider and question it. In raw terms, they seek to pick holes in it. And if there aren’t any then the research – and its particular truth – must be strong within the context of what is known at that time. The process isn’t about advancing the career of the individual scientist; rather assuring the integrity of each piece of research as an individual building block within a global edifice comprising thousands and thousands of researchers; all of whom are subject to the same system of checks.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Angharad’s research question – her hypothesis – concerned hyaenas (ancient and modern) and specifically their responses to environmental changes. The Spotted Hyaena Crocuta crocuta was present in our landscape for much of the Quaternary period – for over half a million years.It survived all manner of conditions both warm and cold and, of course, is still present in Africa where it is the most common large carnivore.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "We tend to view the hyaena as a bit of a slinker, an evil beast, even though in truth it hunts as much as the lion – the so-called ‘King of the Beasts’ – which actually pinches carcasses as often as the hyaena does. However, we should perhaps rethink this stereotype and instead celebrate the hyaena as a highly adaptable – and because of this a very successful – species from which we could learn a great deal about how to survive.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "Nevertheless Angharad’s research, undertaken over a period of four years, examined the size of hyaena bones and teeth in order to reconstruct body size and past diets (especially bone consumption). These findings along with data from the scientific literature were used to attempt to determine the possible causes of the hyaena’s disappearance from Europe.The fossil specimens which she studied in the course of this work are held in museum collections all over England and Wales – and beyond.";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "This shows us that whilst museums are undoubtedly places of wonder, they’re also really important as treasure houses – of knowledge relating to things, much of which we don’t yet know. And who can tell what or where the answers to future questions will come from?";
                storyText.text += "\n";storyText.text += "\n";
                storyText.text += "So; science and museums are all part of the same vital system which, as planet Earth comes under increasing pressure, has come to play a pivotal role in keeping the world – and us as part of it – ticking over. They are the engine and components in a vast Truth Machine which is constantly evolving – but which, being shaped by humans, can occasionally fall prey to human foibles if there aren’t safeguards in place. Modern science though has recognised this and evolved in response. And, of course, it will continue to do so, partly because it acknowledges that everything is always changing – but also because no one individual, in the pursuit of power or personal gain, can be allowed to corrupt its hard won Truth.";
                audioFile = "Homotherium 13";
                break;
            case "14":
                messageText.text = "<align=center><size=80%>WELL DONE!\n<size=75%>YOU HAVE BROUGHT THE TRUTH MACHINE BACK TO LIFE AGAIN!</size></align>";
                showText.alpha = 0;
                break;
            case "15":
                messageText.text = "<size=80%>BRING ERNIE BACK TO LIFE!\n\nAnd when you are ready, click then button and set off round the lake.</size>";
                showText.alpha = 0;
                goBack.alpha = 0;
                
                break;
            
                
        }

        
        rate = 5f;
        sliderValue = 30;
        disc = GameObject.Find("CanvasDisc").GetComponent<Image>();
        discRect = GameObject.Find("CanvasDisc");
        if (PlayerPrefs.GetString("BeastName") == "Billy")
        {
            discRect.transform.localPosition = new Vector3(0, -350, 0);
        }
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
        squeakPlayer = GameObject.Find("SqueakPlayer").GetComponent<AudioSource>();
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        holder = GameObject.Find("Holder");
        cat = GameObject.Find("Cat");
       
        outerTooth = GameObject.Find("OuterTooth");
        innerTooth = GameObject.Find("InnerTooth");
        outerToothHolder = GameObject.Find("OuterToothHolder");
        innerToothHolder = GameObject.Find("InnerToothHolder");
        longToothHolder = GameObject.Find("LongToothHolder");
        longTooth = GameObject.Find("LongTooth");
        molarToothHolder = GameObject.Find("MolarToothHolder");
        molarTooth = GameObject.Find("MolarTooth");
        middleCatHolder = GameObject.Find("MiddleCat");
        catHolder = GameObject.Find("CatHolder");
        
        button = GameObject.Find("CanvasDisc").GetComponent<Image>();
        buttonX = button.transform.position.x;
        buttonY = button.transform.position.y;
        buttonRectTransform = button.rectTransform;
        Vector3 discPosition = Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position);
        cat.transform.SetPositionAndRotation(discPosition, Quaternion.Euler(0, 0, 0));
        outerTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        innerTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        longTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        molarTooth.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));
        //middleCatHolder.transform.SetPositionAndRotation(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position), Quaternion.Euler(0, 0, 0));

        if(animationToPlay == "15")
        {
            middleCatHolder.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            middleCatHolder.transform.localPosition = new Vector3(0f, 1f, 0f);
        }
        if (animationToPlay == "2" || animationToPlay == "5")
        {
            middleCatHolder.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            
        }
        if (animationToPlay == "6" )
        {
            middleCatHolder.transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);

        }
        Debug.Log(Screen.height);
        Debug.Log(Screen.width);
        Debug.Log((float)Screen.height / (float)Screen.width);
        float aspect = (float)Screen.height / (float)Screen.width;
        Debug.Log(aspect);
        if (aspect < 1.4)
        {
            r = 1.3f;
        }
        else
        {
            r = 1.1f;
        }
        StartCoroutine(Clone());
        catAnimator = catHolder.GetComponentsInChildren<Animator>();
        middleCat = GameObject.Find("MiddleCat").GetComponent<Animator>();
        Debug.Log("Playing " + "therium " + animationToPlay);
        middleCat.Play("therium" + animationToPlay);
        StartCoroutine(Friction(0.1f));

    }
    private void PlaySqueak()
    {
        Debug.Log("Playing the squeak");
        squeakClip = Resources.Load<AudioClip>("Audio/squeak");
        squeakPlayer.clip = squeakClip;
        squeakPlayer.PlayOneShot(squeakClip);
    }

    public IEnumerator LoadScene(string sceneName)
    {

        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(sceneName);

    }

    public void ShowScroller()
    {
       
        {
            Debug.Log("Scroller");
            if (scrollView.transform.position.x < 0)
            {
                scrollView.transform.localPosition = new Vector3(0, 500, 0);
            }
            else
            {
                scrollView.transform.localPosition = new Vector3(-2000, 0, 0);
            }
        }
    }

    IEnumerator Clone()
    {

        for (int i = 0; i < 12; i++)
        {
            Color newColour = new Color(1 - (i * 0.2f), 0, 0, 1f);


            GameObject catClone = Instantiate(cat, new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x + (r*1.5f * Mathf.Cos(i * 30 * Mathf.PI / 180)), Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y + (r*1.5f * Mathf.Sin(i * 30 * Mathf.PI / 180)), -9), Quaternion.Euler(0, 0, cat.transform.rotation.z + (i * 30) + 90));

            catClone.name = "catClone-" + (i + 1);
            SpriteRenderer catSprite = catClone.GetComponent<SpriteRenderer>();
            //catSprite.color = newColour;
            // Debug.Log(catClone.name);
            catClone.transform.parent = catHolder.transform;
        }

        for (int i = 0; i < 16; i++)
        {
            GameObject innerToothClone = Instantiate(innerTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r*0.8f * Mathf.Cos(i * 22.5f * Mathf.PI / 180)), Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r*0.8f * Mathf.Sin(i * 22.5f * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, innerTooth.transform.rotation.z + (i * 22.5f) + 90));
            GameObject outerToothClone = Instantiate(outerTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r * Mathf.Cos(i * 22.5f * Mathf.PI / 180)), Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r * Mathf.Sin(i * 22.5f * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, outerTooth.transform.rotation.z + (i * 22.5f) + 90));
            outerToothClone.name = "outerToothClone-" + (i + 1);
            outerToothClone.transform.parent = outerToothHolder.transform;
            innerToothClone.name = "innerToothClone-" + (i + 1);
            innerToothClone.transform.parent = innerToothHolder.transform;

        }

        for (int i = 0; i < 4; i++)
        {
            GameObject longToothClone = Instantiate(longTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r * 0.5f * Mathf.Cos(i * 90 * Mathf.PI / 180)),
                Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r * 0.5f * Mathf.Sin(i * 90 * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, longTooth.transform.rotation.z + (i * 90) +45 ));
            GameObject molarToothClone = Instantiate(molarTooth,
                new Vector3(Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).x
                + (r * 0.5f * Mathf.Cos(i * 90 * Mathf.PI / 180)),
                Camera.main.ScreenToWorldPoint(disc.rectTransform.transform.position).y
                + (r * 0.5f * Mathf.Sin(i * 90 * Mathf.PI / 180)), -9),
                Quaternion.Euler(0, 0, molarTooth.transform.rotation.z + (i * 90) +180));

            longTooth.name = "longToothClone-" + (i + 1);
            longToothClone.transform.parent = longToothHolder.transform;
            molarTooth.name = "molarToothClone-" + (i + 1);
            molarToothClone.transform.parent = molarToothHolder.transform;
        }
        molarToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), 0);
        yield return new WaitForEndOfFrame();
    }
    public void PlayAudio()
    {

        //  StartCoroutine(Scroll(scrollerText, new Vector3(0, 14000, 0), 210f));
        if (animationToPlay == "15")
        {
            StartCoroutine(LoadScene("Text"));
        }
        else
        {
            if (soundPlayer.isPlaying == false)
            {

                audioClip = Resources.Load<AudioClip>("Audio/" + audioFile);
                soundPlayer.clip = audioClip;
                Debug.Log(audioFile);
                Debug.Log(audioClip);
                soundPlayer.Play();
            }
            else
            {

                soundPlayer.Pause();
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {


        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Debug.Log("Touch");
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit " + raycastHit.collider.name);

                int found = raycastHit.collider.name.IndexOf("_");
                string number = raycastHit.collider.name.Substring(found + 1);
                string[] thing = raycastHit.collider.name.Split("_");
               
                if (raycastHit.collider.name == "Cat")
                {
                   
                        PlayAudio();
                    
                }

        } }
        if (playTheSqueak == true)
        {
            PlaySqueak();
            playTheSqueak = false;
        }
        if (rate > 0)
        {
            catHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), rotationAmount * (rate / 2));
            outerToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), -rotationAmount * (rate/1.75f));
            innerToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), rotationAmount * (rate/1.5f));
            longToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), -rotationAmount * (rate / 1.25f));
            molarToothHolder.transform.RotateAround(Camera.main.ScreenToWorldPoint(buttonRectTransform.transform.position), new Vector3(0, 0, 1f), -rotationAmount * (rate));
        }
        foreach (Animator anim in catAnimator)
        {
            anim.speed = rate;
        }
        middleCat.speed = rate/2;

    }
    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;

        int n = 0;
        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3F;
            amp *= 0.3F;
            n++;
        }
    }
    IEnumerator Friction(float time)
    {
        yield return new WaitForSeconds(time);
        while (true)
        {
            if (rate > 0)
            {
                running = true;
                rate -= friction;
                if (bpm > 100) bpm -= 1;
            }
            else
            {
                rate = 0;
                running = false;
            }
            yield return new WaitForSeconds(time);
        }
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {


        if (data.FingerUp == true || data.FingerStationary == true)
        {
            //rate = 0;
            friction = 0.3f;


        }


        float speed = (data.EndPosition - data.StartPosition).magnitude / sliderValue;
        
        if (speed > speedLimit) speed = speedLimit;


        if (data.StartPosition.y < buttonY + 500)
        {

            if (data.StartPosition.x < buttonX && data.EndPosition.x > buttonX && data.StartPosition.y < buttonY && data.EndPosition.y < buttonY)
            {
                Debug.Log("Crossed Lower Midpoint X" + data.StartPosition.x.ToString() + ":" + data.EndPosition.x.ToString());
                Debug.Log("Crossed Lower Midpoint Y" + data.StartPosition.y.ToString() + ":" + data.EndPosition.y.ToString());
                rate = speed;
                bpm = speed * tempo;
            }
            if (data.StartPosition.x > buttonX && data.EndPosition.x < buttonX && data.StartPosition.y > buttonY && data.EndPosition.y > buttonY)
            {
                Debug.Log("Crossed Upper Midpoint X" + data.StartPosition.x.ToString() + ":" + data.EndPosition.x.ToString());
                Debug.Log("Crossed Upper Midpoint Y" + data.StartPosition.y.ToString() + ":" + data.EndPosition.y.ToString());
                playTheSqueak = true;
                rate = speed;
                bpm = speed * tempo;
            }
            if (data.StartPosition.y < buttonY && data.EndPosition.y > buttonY && data.StartPosition.x > buttonX && data.EndPosition.x > buttonX)
            {
                Debug.Log("Crossed Right Midpoint");
                rate = speed;
                bpm = speed * tempo;
            }
            if (data.StartPosition.y > buttonY && data.EndPosition.y < buttonY && data.StartPosition.x < buttonX && data.EndPosition.x < buttonX)
            {
                Debug.Log("Crossed Left Midpoint");
                rate = speed;
                bpm = speed * tempo;
            }

            if (data.StartPosition.x > buttonX && data.StartPosition.y < buttonY)
            {
                if (data.StartPosition.y < data.EndPosition.y && data.StartPosition.x < data.EndPosition.x)
                {

                    Debug.Log("Going AntiClock in the Bottom Right Quad");

                    rate = speed;
                    bpm = speed * tempo;

                }

            }



            if (data.StartPosition.x > buttonX && data.StartPosition.y > buttonY)
            {

                if (data.StartPosition.y < data.EndPosition.y && data.StartPosition.x > data.EndPosition.x)
                {
                    Debug.Log("Going AntiClock In Top Right Quad    ");
                    rate = speed;
                    bpm = speed * tempo;

                }

            }

            if (data.StartPosition.x < buttonX && data.StartPosition.y > buttonY)
            {

                if (data.StartPosition.y > data.EndPosition.y && data.StartPosition.x > data.EndPosition.x)
                {
                    Debug.Log("Going AntiClock In Top Left Quad ");
                    rate = speed;
                    bpm = speed * tempo;

                }
            }
            if (data.StartPosition.x < buttonX && data.StartPosition.y < buttonY)
            {

                if (data.StartPosition.y > data.EndPosition.y && data.StartPosition.x < data.EndPosition.x)
                {
                    Debug.Log("Going AntiClock In Bottom Left Quad ");
                    rate = speed;
                    bpm = speed * tempo;

                }
            }
        }
        Debug.Log("Speed Limit is" + speedLimit.ToString());
        Debug.Log("Rate is " + rate.ToString());
        Debug.Log("Speed is " + speed.ToString());

    }
}
