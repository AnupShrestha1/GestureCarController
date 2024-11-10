import cv2
import mediapipe as mp
import win32pipe, win32file, pywintypes
import math

# Initialize MediaPipe hand detection
mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils

# Named pipe settings
PIPE_NAME = r'\\.\pipe\UnityPipe'

# Mapping gestures to control integers (as per Unity's Control enum)
controls = {
    "ACCELERATE": 1,
    "ACCELERATE_RIGHT": 2,
    "ACCELERATE_LEFT": 3,
    "BRAKE": 4,
    "BRAKE_RIGHT": 5,
    "BRAKE_LEFT": 6,
    "RIGHT": 7,
    "LEFT": 8,
    "HAND_BRAKE": 9,
    "NONE": 0
}

def send_message_to_unity(message):
    """Send a message (integer) to Unity through a named pipe."""
    try:
        pipe = win32pipe.CreateNamedPipe(
            PIPE_NAME,
            win32pipe.PIPE_ACCESS_OUTBOUND,
            win32pipe.PIPE_TYPE_MESSAGE | win32pipe.PIPE_WAIT,
            1, 65536, 65536,
            0,
            None
        )
        
        win32pipe.ConnectNamedPipe(pipe, None)
        win32file.WriteFile(pipe, str.encode(str(message)))  # Send integer as string
        win32file.CloseHandle(pipe)

    except pywintypes.error as e:
        print(f"Error communicating with Unity: {e}")

def is_fist(landmarks):
    """Check if the hand is making a fist by comparing distances between fingertips and knuckles."""
    finger_knuckle_pairs = [(8, 6), (12, 10), (16, 14), (20, 18)]  # Index, middle, ring, pinky

    # Check if each fingertip is close to its corresponding knuckle
    for tip, knuckle in finger_knuckle_pairs:
        distance = calculate_distance(landmarks[tip], landmarks[knuckle])
        if distance > .07:  # Set a threshold for how close the fingertip and knuckle should be
            return False  # If any finger is not bent enough, it's not a fist

    # Optional: Check if the thumb is tucked (thumb tip near the palm/wrist)
    # thumb_tip = landmarks[4]
    # wrist = landmarks[0]
    # thumb_wrist_distance = calculate_distance(thumb_tip, wrist)
    # if thumb_wrist_distance > 0.2:  # Adjust this threshold based on hand size
    #     return False

    return True

def calculate_distance(point1, point2):
    """Calculate Euclidean distance between two points."""
    return math.sqrt((point1.x - point2.x) ** 2 + (point1.y - point2.y) ** 2)

def is_index_out(landmarks):
    """Check if the hand gesture has the index finger out and other fingers curled."""
    # Index finger is straight, other fingers are bent
    return (landmarks[8].y < landmarks[6].y) and all(landmarks[tip].y > landmarks[tip - 2].y for tip in [12, 16, 20])

def detect_gesture(landmarks):
    """Detect hand gesture based on landmarks and return control integer."""
    index_tip = landmarks[8]
    wrist = landmarks[0]

    # Get hand leaning direction
    hand_lean_left = index_tip.x < wrist.x - 0.15  # Lean left
    hand_lean_right = index_tip.x > wrist.x + 0.05  # Lean right

    # 3. Brake: Fist (all fingers bent)
    if is_fist(landmarks):
        if hand_lean_left:
            return controls["BRAKE_LEFT"]
        elif hand_lean_right:
            return controls["BRAKE_RIGHT"]
        else:
            return controls["BRAKE"]
        
    # 2. Steer left or right: Index finger out (lean left or right)
    if is_index_out(landmarks):
        if hand_lean_left:
            return controls["LEFT"]
        elif hand_lean_right:
            return controls["RIGHT"]

    # 1. Accelerate: Open palm (all fingers straight and above wrist)
    if all(landmark.y < wrist.y for landmark in [landmarks[8], landmarks[12], landmarks[16], landmarks[20]]):
        if hand_lean_left:
            return controls["ACCELERATE_LEFT"]
        elif hand_lean_right:
            return controls["ACCELERATE_RIGHT"]
        else:
            return controls["ACCELERATE"]

    return controls["NONE"]

def main():
    """Main loop to capture video and detect gestures."""
    cap = cv2.VideoCapture(0)

    with mp_hands.Hands(
        max_num_hands=1,
        min_detection_confidence=0.7,
        min_tracking_confidence=0.7
    ) as hands:
        while cap.isOpened():
            success, frame = cap.read()
            if not success:
                print("Ignoring empty camera frame.")
                continue

            # Flip the image horizontally for a selfie-view display
            frame = cv2.flip(frame, 1)

            # Convert the BGR image to RGB
            image_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            image_rgb.flags.writeable = False

            # Detect hand landmarks
            results = hands.process(image_rgb)

            # Draw the hand annotations on the image
            image_rgb.flags.writeable = True
            image_bgr = cv2.cvtColor(image_rgb, cv2.COLOR_RGB2BGR)

            if results.multi_hand_landmarks:
                for hand_landmarks in results.multi_hand_landmarks:
                    mp_drawing.draw_landmarks(
                        image_bgr, hand_landmarks, mp_hands.HAND_CONNECTIONS)

                    # Detect gesture and send control integer to Unity
                    gesture = detect_gesture(hand_landmarks.landmark)
                    print(f"Detected Gesture Control: {gesture}")
                    send_message_to_unity(gesture)

            # Display the frame with hand landmarks
            height, width=image_bgr.shape[:2]
            resized_image= cv2.resize(image_bgr,(width//2,height//2))
            cv2.imshow('Hand Gesture Detection', resized_image)
            
            # Exit when 'q' is pressed
            if cv2.waitKey(5) & 0xFF == ord('q'):
                break

    cap.release()
    cv2.destroyAllWindows()

if __name__ == "__main__":
    main()
