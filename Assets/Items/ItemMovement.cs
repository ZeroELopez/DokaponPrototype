    float rotationSpeed;

void SetInputs(){
inputs.Player.movement.performed += onDirection;
inputs.Player.Accept.performed += Accept;
}

void UnsetInputs(){
inputs.Player.movement.performed -= onDirection;
inputs.Player.Accept.performed -= Accept;
player.SetInputs(true);
}

void onDirection(InputAction.CallbackContext context)
    {
        Vector2 floatDir = context.ReadValue<Vector2>();
        
rotation += floatDir.x * rotationSpeed;        
    }

    void Confirm(InputAction.CallbackContext context)
    {

       
    }
    

    }