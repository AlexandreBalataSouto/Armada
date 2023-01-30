using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCoordinator : MonoBehaviour
{
    //The laser coordinator

    [SerializeField] private Kraken _kraken;
    private List<LaserPoint> _laserPoints;
    private bool _isCourutineRunning = false;
    private List<string> _patrons = new List<string>();
    private string _thisCoroutine;
    private int index;
    public bool IsSecretShootActive;

    void Start()
    {
        _laserPoints = new List<LaserPoint>(gameObject.GetComponentsInChildren<LaserPoint>());
        _patrons.Add("Patron01");
        _patrons.Add("Patron02");
        _patrons.Add("Patron03");
        _patrons.Add("Patron04");

        if(_kraken.gameObject.activeSelf)
        {
            IsSecretShootActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isCourutineRunning && _kraken.gameObject.activeSelf)
        {
            index = Random.Range(0, _patrons.Count);

            _thisCoroutine = _patrons[index];

            StartCoroutine(_thisCoroutine);
        }

        if (!_kraken.gameObject.activeSelf)
        {
            IsSecretShootActive = false;
            StopCoroutine(_thisCoroutine);
        }
    }

    IEnumerator Patron01()
    {
        _isCourutineRunning = true;
        _laserPoints[0].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[1].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[2].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[3].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[5].Shoot();
        yield return new WaitForSeconds(1f);
        _isCourutineRunning = false;
    }
    IEnumerator Patron02()
    {
        _isCourutineRunning = true;
        _laserPoints[0].Shoot();
        _laserPoints[6].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[1].Shoot();
        _laserPoints[5].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[2].Shoot();
        _laserPoints[4].Shoot();
        yield return new WaitForSeconds(1f);
        _isCourutineRunning = false;
    }
    IEnumerator Patron03()
    {
        _isCourutineRunning = true;
        _laserPoints[1].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[5].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[3].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[6].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[2].Shoot();
        yield return new WaitForSeconds(1f);
        _isCourutineRunning = false;
    }
    IEnumerator Patron04()
    {
        _isCourutineRunning = true;
        _laserPoints[6].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[5].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[4].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[3].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[2].Shoot();
        yield return new WaitForSeconds(0.5f);
        _laserPoints[1].Shoot();
        yield return new WaitForSeconds(1f);
        _isCourutineRunning = false;
    }
}
