using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody rigidbody;

    Vector3 forwardVelocity = new Vector3(7, 0, 0);
    float jumpVelocity = 18;

    public bool isGrounded = true;
    public bool didDoubleJump = false;
    public bool isLeftSideCollided = false;
    public bool isRightSideCollided = false;

    public bool isAirSliding = false;
    public bool brokeAirSlideWithJump = false;

    float airSlideConsumtion = 100;
    float airslidepower = 100;
    public Image sirSlidePowerBar;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float dpad = Input.GetAxis("Horizontal");
        float stick = Input.GetAxis("HorizontalStick");

        if (Input.GetKey(KeyCode.A) || dpad < 0 || stick < 0) { moveLeft(); }
        else if (Input.GetKey(KeyCode.D) || dpad > 0 || stick > 0) { moveRight(); }
        else{stopLeftRightMovement();}

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1)) {jump(); }


        if (brokeAirSlideWithJump == false && airslidepower > 0)
        {
            //airsliding
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Joystick1Button5))
            {
                dashRight();
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Joystick1Button4))
            {
                dashLeft();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Joystick1Button5))
            {
                moveRight();
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Joystick1Button4))
            {
                moveLeft();
            }
        }

        //release airslide
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            isAirSliding = false;
            brokeAirSlideWithJump = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.Joystick1Button4))
        {
            isAirSliding = false;
            brokeAirSlideWithJump = false;
        }

        if (isAirSliding == false || isGrounded == true)
        {
            if (airslidepower < 100)
            {
                airslidepower += (airSlideConsumtion/2) * Time.deltaTime;
                sirSlidePowerBar.rectTransform.sizeDelta = new Vector2(airslidepower, sirSlidePowerBar.rectTransform.rect.height);
                sirSlidePowerBar.rectTransform.anchoredPosition = new Vector2(sirSlidePowerBar.rectTransform.anchoredPosition.x + ((airSlideConsumtion/2) * Time.deltaTime), sirSlidePowerBar.rectTransform.anchoredPosition.y);
            }
            else
            {
                airslidepower = 100;
            }
        
        
        }
        else if (isAirSliding)
        {
            if (isGrounded == false)
            {
                if (airslidepower > 0)
                {
                    airslidepower -= airSlideConsumtion * Time.deltaTime;
                    sirSlidePowerBar.rectTransform.sizeDelta = new Vector2(airslidepower, sirSlidePowerBar.rectTransform.rect.height);
                    sirSlidePowerBar.rectTransform.anchoredPosition = new Vector2(sirSlidePowerBar.rectTransform.anchoredPosition.x - (airSlideConsumtion * Time.deltaTime), sirSlidePowerBar.rectTransform.anchoredPosition.y);
                }
                else
                {
                    airslidepower = 0;
                }
            }
        }
	}


    void moveRight()
    {
            if (isRightSideCollided == false)
            {
                Vector3 velocity = rigidbody.velocity;
                velocity.x = forwardVelocity.x;
                rigidbody.velocity = velocity;
            }
            else
            {
                Vector3 velocity = rigidbody.velocity;
                velocity.x = 0;
                rigidbody.velocity = velocity;
            }
        
    }
    void moveLeft()
    {
        if (isLeftSideCollided == false)
        {
            Vector3 velocity = rigidbody.velocity;
            velocity.x = -forwardVelocity.x;
            rigidbody.velocity = velocity;
        }
        else
        {
            Vector3 velocity = rigidbody.velocity;
            velocity.x = 0;
            rigidbody.velocity = velocity;
        }
    }
    void stopLeftRightMovement()
    {
        Vector3 velocity = rigidbody.velocity;
        velocity.x = 0;
        rigidbody.velocity = velocity;
    }


    void dashRight()
    {
        rigidbody.velocity = forwardVelocity * 2;
        isAirSliding = true;
    }
    void dashLeft()
    {
        rigidbody.velocity = -forwardVelocity * 2;
        isAirSliding = true;
    }

    void jump()
    {
        if (isAirSliding && didDoubleJump == false)
        {
            brokeAirSlideWithJump = true;
            isAirSliding = false;
        }

        if (isGrounded)
        {
            Vector3 velocity = rigidbody.velocity;
            velocity.y = jumpVelocity;
            rigidbody.velocity = velocity;
            isGrounded = false;
        }
        else
        {
            if (didDoubleJump == false)
            {
                Vector3 velocity = rigidbody.velocity;
                velocity.y = jumpVelocity;
                rigidbody.velocity = velocity;
                didDoubleJump = true;
            }
        }
    }


    public void setIsGrounded(bool grounded)
    {
        isGrounded = grounded;

        if (grounded)
        {
            didDoubleJump = false;
        }
    }
    public void setLeftSide(bool collided)
    {
        isLeftSideCollided = collided;
    }
    public void setRightSide(bool collided)
    {
        isRightSideCollided = collided;
    }

}
