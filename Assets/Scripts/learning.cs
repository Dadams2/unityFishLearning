using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class learning : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    public float stepSpeed = 0.5f;
    public double a = 0.1;
    public double y = 0.5;
    Vector3 movement;
    private Vector3 end = new Vector3(-4.5f, 0.5f, 4.5f);
    private Vector3 start = new Vector3(-4.5f, 0.5f, -4.5f);
    //things for TD update
    public double[,] state = new double[10,10];

    public double[,] reward = new double[10,10];

    //translate in game coordinates to state representation
    int t(double x){
        return (int)(x +4.5);
    }
    void Start()
    {
        //Debug.Log(reward.Length.ToString() + " " + state.Length.ToString());
        for(int i = 0; i < reward.Rank; i++){
            for(int j = 0; j < reward.Rank; j++){
                //Debug.Log(i.ToString() + j.ToString());
                reward[i,j] = -0.1;
            }
        }
        //setting up the goal of reward
        reward[0,9] = 50;
        InvokeRepeating("move", 0.1f, stepSpeed);
    }

    // Update is called once per frame
    void move()
    {
        //really crappy way of dealing with end condition

        if(this.transform.position == end){
            this.transform.position += Vector3.forward;
            this.transform.position = start;
        }

        //float step = (moveSpeed * Time.deltaTime)/stepSpeed;
        //get best move based on our actions
        movement = findBestMove();
        //get a move that will work if all moves equal
        if(movement == Vector3.zero){
            //Debug.Log("taking random move");
            movement = randomMove();
        }
        Vector3 moveto = transform.position+movement;
        //Debug.Log(transform.position.ToString() + " " + moveto.ToString());
        //transform.position = Vector3.MoveTowards(transform.position, moveto, step);
        updateValues(transform.position.x, transform.position.y, moveto.x, moveto.y);
        transform.position = moveto;
    }

    Vector3 findBestMove() {
        // [up, down, left, right]
        double[] moves = new double[4];
        //default values wil alwyas be zero
        if(FindAt(transform.position, Vector3.forward).tag != "Wall"){
            moves[0] = state[t(transform.position.x), t(transform.position.y)+1];
        }
        if(FindAt(transform.position, Vector3.back).tag != "Wall"){
            moves[1] = state[t(transform.position.x), t(transform.position.y)-1];
        }
        if(FindAt(transform.position, Vector3.left).tag != "Wall"){
            moves[2] = state[t(transform.position.x)-1, t(transform.position.y)];
        }
        if(FindAt(transform.position, Vector3.right).tag != "Wall"){
            moves[3] = state[t(transform.position.x)+1, t(transform.position.y)-1];
        }
        bool issame = true;
        int move =  0;
        double max = moves[0];
        for(int i = 1; i<moves.Rank; i++){
            if(moves[i] > max){
                max = moves[i];
                move = i;
                issame = false;
            }
        }
        //checking for if needed to make random move
        if(issame){
            return Vector3.zero;
        }
        switch(move) {
            case 0:
                movement = Vector3.forward;
                break;
            case 1:
                movement = Vector3.back;
                break;
            case 2:
                movement = Vector3.left;
                break;
            case 3:
                movement = Vector3.right;
                break;
        }
        return movement;
    }

    Vector3 randomMove() {
        movement = Vector3.zero;
        while(movement == Vector3.zero){
            int num = Random.Range(0,4);
            switch(num) {
                case 0:
                    //up
                    if(FindAt(transform.position, Vector3.back).tag != "Wall"){
                        movement = Vector3.back;
                    }
                    else {
                        //Debug.Log("tried back");
                    }
                    break;
                case 1:
                    if(FindAt(transform.position, Vector3.forward).tag != "Wall"){
                        movement = Vector3.forward;
                    }
                    else {
                        //Debug.Log("tried forward");
                    }
                    break;
                case 2:
                    if(FindAt(transform.position, Vector3.left).tag != "Wall"){
                        movement = Vector3.left;
                    }
                    else {
                        //Debug.Log("tried left");
                    }
                    break;
                case 3:
                    if(FindAt(transform.position, Vector3.right).tag != "Wall"){
                        movement = Vector3.right;
                    }
                    else {
                        //Debug.Log("tried right");
                    }
                    break;
            }
        }
        return movement;
    }


    void updateValues(double x1, double y1, double x2, double y2) {
        //translate values
        state[t(x1),t(y1)] += a*(reward[t(x2),t(y2)] + (y * state[t(x2),t(y2)]) - state[t(x1),t(y1)]);
    }

    GameObject FindAt(Vector3 pos, Vector3 direction) {
        RaycastHit hit;
        // cast a ray downwards with range = 0.5 which is always where Wall will be
        if (Physics.Raycast(pos, direction, out hit, 0.5f)){
            return hit.collider.gameObject;
        } else {
            return this.gameObject;
        }
    }
}
