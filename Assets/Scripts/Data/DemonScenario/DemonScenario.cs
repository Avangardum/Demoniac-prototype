using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DemonScenario", menuName = "DemonScenario")]
public class DemonScenario : ScriptableObject, IEnumerable<DemonScenarioElement>
{
    [SerializeField] private List<DemonScenarioElement> elements;

    public DemonScenarioElement this[int index] => elements[index];

    public IEnumerator<DemonScenarioElement> GetEnumerator()
    {
        return ((IEnumerable<DemonScenarioElement>)elements).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<DemonScenarioElement>)elements).GetEnumerator();
    }

    public void Delete(DemonScenarioElement element) => elements.Remove(element);

    public void Add(DemonScenarioElement element)
    {
        elements.Add(element);
        elements = elements.OrderBy(x => x.Timing).ToList();
    }

    public Queue<DemonScenarioElement> ToQueue()
    {
        Queue<DemonScenarioElement> queue = new Queue<DemonScenarioElement>();
        foreach(var element in elements)
        {
            queue.Enqueue(element);
        }
        return queue;
    }
}
