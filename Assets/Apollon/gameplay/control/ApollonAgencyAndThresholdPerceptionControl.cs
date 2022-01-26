// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/AgencyAndThresholdPerceptionControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Labsim.apollon.gameplay.control
{
    public class @ApollonAgencyAndThresholdPerceptionControl : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @ApollonAgencyAndThresholdPerceptionControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""AgencyAndThresholdPerceptionControl"",
    ""maps"": [
        {
            ""name"": ""Subject"",
            ""id"": ""5e5eeb7d-564e-41ba-bae6-d3274ecff809"",
            ""actions"": [
                {
                    ""name"": ""ValueChanged"",
                    ""type"": ""Value"",
                    ""id"": ""594659e9-c85e-4bed-a0d8-6a62b73b6372"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NeutralCommandTriggered"",
                    ""type"": ""Value"",
                    ""id"": ""153a7b74-0af4-4525-89c2-a221afa3d752"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PositiveCommandTriggered"",
                    ""type"": ""Value"",
                    ""id"": ""3dd67d32-fcc0-4442-a028-41351e9811c6"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NegativeCommandTriggered"",
                    ""type"": ""Value"",
                    ""id"": ""f50fde1b-12aa-4fc4-a810-4aba61f066e9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UserResponse"",
                    ""type"": ""Button"",
                    ""id"": ""79b902d3-4436-48fd-99f9-7ebec20c81a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c8e08c74-2fb2-4ad5-b595-5324fb7314bf"",
                    ""path"": ""<HID::Thrustmaster Throttle - HOTAS Warthog>/z"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Cockpit"",
                    ""action"": ""ValueChanged"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0315e7e5-f979-415d-9979-4aacbaa0fa98"",
                    ""path"": ""<HID::Thrustmaster Throttle - HOTAS Warthog>/z"",
                    ""interactions"": ""ApollonHoldInRangeWithThreshold(m_duration_ms=250,m_threshold_ratio_percentage=15,m_lower_bound=-0.05,m_upper_bound=0.23)"",
                    ""processors"": ""AxisDeadzone,Invert"",
                    ""groups"": ""Cockpit"",
                    ""action"": ""NeutralCommandTriggered"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8df495dc-8f62-477a-984a-7c29897ed59b"",
                    ""path"": ""<HID::Thrustmaster Throttle - HOTAS Warthog>/z"",
                    ""interactions"": ""ApollonHoldInRangeWithThreshold(m_duration_ms=50,m_lower_bound=0.75)"",
                    ""processors"": ""AxisDeadzone,Invert"",
                    ""groups"": ""Cockpit"",
                    ""action"": ""PositiveCommandTriggered"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2faf3f05-c0c7-4b54-9139-cde36c27b654"",
                    ""path"": ""<HID::Thrustmaster Throttle - HOTAS Warthog>/z"",
                    ""interactions"": ""ApollonHoldInRangeWithThreshold(m_duration_ms=50,m_upper_bound=-0.75)"",
                    ""processors"": ""AxisDeadzone,Invert"",
                    ""groups"": ""Cockpit"",
                    ""action"": ""NegativeCommandTriggered"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a795ed0a-f0ba-4a0f-ad1f-10ab4370fe59"",
                    ""path"": ""<HID::Thrustmaster Throttle - HOTAS Warthog>/button15"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Cockpit"",
                    ""action"": ""UserResponse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Supervisor"",
            ""id"": ""6b42688d-4154-4e21-bacc-4fa9979550a7"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""f067eafa-2b1d-410b-acb2-35583f2f818b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""05705115-7ef4-4fb4-962a-20990fac0716"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""139058ab-aa00-4379-bb94-04b44d32af60"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4c5926c3-3935-4455-b0c8-2a1f57a3c97d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b4ea069d-7a91-4c7b-9c07-d5ae9998c018"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d445de0d-72ff-464f-9e94-5a581c928414"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b0c020d1-3471-41b8-b4b2-d79ad3506331"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1fe07375-142d-474d-88d6-e94633ce6e8a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5209c99e-b0d0-4dda-a700-1e132c378530"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2d4c890d-9186-415b-ae97-59e8e235f54e"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""7176e101-dd03-4a92-a43e-b1dd7da3b89d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""09e5cf60-d62c-438c-bc89-8603f4b08b25"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6f536862-26d6-4081-aa44-8981f9ff7dfc"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b8865fa1-89d4-44d2-831a-6ce1a732f187"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6f5f9ad2-3c5d-4822-8355-cbe1599e5dd6"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7ffaf351-9b91-4ffb-a78a-9eaa967c25ba"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""eb1bac54-2d99-48f3-a9b9-5b07984ba949"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c76785fe-bfd4-4a02-86f3-aa2d42f4fded"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""687ea193-5890-49f0-9d08-434b81d7f5bc"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""869f24a5-7999-4468-a2a2-59f4cda34fc3"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""9315a392-3221-430b-81fc-f4320c286743"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a898772c-9baa-4702-a2fb-0b44c2293ccb"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0f27670e-a770-446e-84b1-941d90cae69e"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5f07677b-52db-45cf-b665-9d55431d5527"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""33a23bfc-c964-4d28-8bce-ddea12d6c606"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""024f471f-d2b3-4018-b099-807d73b67073"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""458e5d5a-fd36-4558-8ea8-9be5d8a7a1c4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ed6879da-2df2-495d-b999-3819c5412ef6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""73d645f4-6b78-4902-a780-44648f601d80"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""efe634f8-a6e8-4d00-82c4-b4dbf0e3ac0a"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""161f2b39-10b2-4839-b1b8-8bc9b255bdb2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b04637d4-fca0-44ad-8344-65cdd22eba94"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e129cf94-5316-472c-a6e6-354cc6658da3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d41fd6b9-d2cd-4e10-b012-125159ad4772"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""701bc5a2-25ee-4138-b622-635068b1ab32"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ed30f44-34b1-4562-938e-97b244d8750d"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ac43de9-471b-402e-9eac-40304fcc70d4"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fb5db95-fe74-48d8-b1ce-9a498c8af979"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""420f640f-1f4e-482f-896d-295bcfc4a111"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76c0d41b-48da-4354-9406-e503b32ddc3e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11eb3b27-ae94-48c1-9539-fc3a4b421c8b"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""971b06a5-ffe3-48ee-b6f2-f161006af845"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a66baf8b-383a-4b69-accd-8b1c3f3552a9"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4c92c88-ba28-46d5-a495-a2dde18a36d8"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fb933cb-cb84-4152-99be-4583ddbb9731"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1b35879-f8ad-4fe9-9449-e0a2c66440d3"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d057aadb-e4ad-4e4c-89e7-fdcdf890c7f8"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c18b4a1-6d0a-4f7e-be02-bea7e201da44"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Cockpit"",
            ""bindingGroup"": ""Cockpit"",
            ""devices"": [
                {
                    ""devicePath"": ""<HID::Thustmaster Joystick - HOTAS Warthog>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<HID::Thrustmaster Throttle - HOTAS Warthog>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Subject
            m_Subject = asset.FindActionMap("Subject", throwIfNotFound: true);
            m_Subject_ValueChanged = m_Subject.FindAction("ValueChanged", throwIfNotFound: true);
            m_Subject_NeutralCommandTriggered = m_Subject.FindAction("NeutralCommandTriggered", throwIfNotFound: true);
            m_Subject_PositiveCommandTriggered = m_Subject.FindAction("PositiveCommandTriggered", throwIfNotFound: true);
            m_Subject_NegativeCommandTriggered = m_Subject.FindAction("NegativeCommandTriggered", throwIfNotFound: true);
            m_Subject_UserResponse = m_Subject.FindAction("UserResponse", throwIfNotFound: true);
            // Supervisor
            m_Supervisor = asset.FindActionMap("Supervisor", throwIfNotFound: true);
            m_Supervisor_Navigate = m_Supervisor.FindAction("Navigate", throwIfNotFound: true);
            m_Supervisor_Submit = m_Supervisor.FindAction("Submit", throwIfNotFound: true);
            m_Supervisor_Cancel = m_Supervisor.FindAction("Cancel", throwIfNotFound: true);
            m_Supervisor_Point = m_Supervisor.FindAction("Point", throwIfNotFound: true);
            m_Supervisor_Click = m_Supervisor.FindAction("Click", throwIfNotFound: true);
            m_Supervisor_ScrollWheel = m_Supervisor.FindAction("ScrollWheel", throwIfNotFound: true);
            m_Supervisor_MiddleClick = m_Supervisor.FindAction("MiddleClick", throwIfNotFound: true);
            m_Supervisor_RightClick = m_Supervisor.FindAction("RightClick", throwIfNotFound: true);
            m_Supervisor_TrackedDevicePosition = m_Supervisor.FindAction("TrackedDevicePosition", throwIfNotFound: true);
            m_Supervisor_TrackedDeviceOrientation = m_Supervisor.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Subject
        private readonly InputActionMap m_Subject;
        private ISubjectActions m_SubjectActionsCallbackInterface;
        private readonly InputAction m_Subject_ValueChanged;
        private readonly InputAction m_Subject_NeutralCommandTriggered;
        private readonly InputAction m_Subject_PositiveCommandTriggered;
        private readonly InputAction m_Subject_NegativeCommandTriggered;
        private readonly InputAction m_Subject_UserResponse;
        public struct SubjectActions
        {
            private @ApollonAgencyAndThresholdPerceptionControl m_Wrapper;
            public SubjectActions(@ApollonAgencyAndThresholdPerceptionControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @ValueChanged => m_Wrapper.m_Subject_ValueChanged;
            public InputAction @NeutralCommandTriggered => m_Wrapper.m_Subject_NeutralCommandTriggered;
            public InputAction @PositiveCommandTriggered => m_Wrapper.m_Subject_PositiveCommandTriggered;
            public InputAction @NegativeCommandTriggered => m_Wrapper.m_Subject_NegativeCommandTriggered;
            public InputAction @UserResponse => m_Wrapper.m_Subject_UserResponse;
            public InputActionMap Get() { return m_Wrapper.m_Subject; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SubjectActions set) { return set.Get(); }
            public void SetCallbacks(ISubjectActions instance)
            {
                if (m_Wrapper.m_SubjectActionsCallbackInterface != null)
                {
                    @ValueChanged.started -= m_Wrapper.m_SubjectActionsCallbackInterface.OnValueChanged;
                    @ValueChanged.performed -= m_Wrapper.m_SubjectActionsCallbackInterface.OnValueChanged;
                    @ValueChanged.canceled -= m_Wrapper.m_SubjectActionsCallbackInterface.OnValueChanged;
                    @NeutralCommandTriggered.started -= m_Wrapper.m_SubjectActionsCallbackInterface.OnNeutralCommandTriggered;
                    @NeutralCommandTriggered.performed -= m_Wrapper.m_SubjectActionsCallbackInterface.OnNeutralCommandTriggered;
                    @NeutralCommandTriggered.canceled -= m_Wrapper.m_SubjectActionsCallbackInterface.OnNeutralCommandTriggered;
                    @PositiveCommandTriggered.started -= m_Wrapper.m_SubjectActionsCallbackInterface.OnPositiveCommandTriggered;
                    @PositiveCommandTriggered.performed -= m_Wrapper.m_SubjectActionsCallbackInterface.OnPositiveCommandTriggered;
                    @PositiveCommandTriggered.canceled -= m_Wrapper.m_SubjectActionsCallbackInterface.OnPositiveCommandTriggered;
                    @NegativeCommandTriggered.started -= m_Wrapper.m_SubjectActionsCallbackInterface.OnNegativeCommandTriggered;
                    @NegativeCommandTriggered.performed -= m_Wrapper.m_SubjectActionsCallbackInterface.OnNegativeCommandTriggered;
                    @NegativeCommandTriggered.canceled -= m_Wrapper.m_SubjectActionsCallbackInterface.OnNegativeCommandTriggered;
                    @UserResponse.started -= m_Wrapper.m_SubjectActionsCallbackInterface.OnUserResponse;
                    @UserResponse.performed -= m_Wrapper.m_SubjectActionsCallbackInterface.OnUserResponse;
                    @UserResponse.canceled -= m_Wrapper.m_SubjectActionsCallbackInterface.OnUserResponse;
                }
                m_Wrapper.m_SubjectActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ValueChanged.started += instance.OnValueChanged;
                    @ValueChanged.performed += instance.OnValueChanged;
                    @ValueChanged.canceled += instance.OnValueChanged;
                    @NeutralCommandTriggered.started += instance.OnNeutralCommandTriggered;
                    @NeutralCommandTriggered.performed += instance.OnNeutralCommandTriggered;
                    @NeutralCommandTriggered.canceled += instance.OnNeutralCommandTriggered;
                    @PositiveCommandTriggered.started += instance.OnPositiveCommandTriggered;
                    @PositiveCommandTriggered.performed += instance.OnPositiveCommandTriggered;
                    @PositiveCommandTriggered.canceled += instance.OnPositiveCommandTriggered;
                    @NegativeCommandTriggered.started += instance.OnNegativeCommandTriggered;
                    @NegativeCommandTriggered.performed += instance.OnNegativeCommandTriggered;
                    @NegativeCommandTriggered.canceled += instance.OnNegativeCommandTriggered;
                    @UserResponse.started += instance.OnUserResponse;
                    @UserResponse.performed += instance.OnUserResponse;
                    @UserResponse.canceled += instance.OnUserResponse;
                }
            }
        }
        public SubjectActions @Subject => new SubjectActions(this);

        // Supervisor
        private readonly InputActionMap m_Supervisor;
        private ISupervisorActions m_SupervisorActionsCallbackInterface;
        private readonly InputAction m_Supervisor_Navigate;
        private readonly InputAction m_Supervisor_Submit;
        private readonly InputAction m_Supervisor_Cancel;
        private readonly InputAction m_Supervisor_Point;
        private readonly InputAction m_Supervisor_Click;
        private readonly InputAction m_Supervisor_ScrollWheel;
        private readonly InputAction m_Supervisor_MiddleClick;
        private readonly InputAction m_Supervisor_RightClick;
        private readonly InputAction m_Supervisor_TrackedDevicePosition;
        private readonly InputAction m_Supervisor_TrackedDeviceOrientation;
        public struct SupervisorActions
        {
            private @ApollonAgencyAndThresholdPerceptionControl m_Wrapper;
            public SupervisorActions(@ApollonAgencyAndThresholdPerceptionControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @Navigate => m_Wrapper.m_Supervisor_Navigate;
            public InputAction @Submit => m_Wrapper.m_Supervisor_Submit;
            public InputAction @Cancel => m_Wrapper.m_Supervisor_Cancel;
            public InputAction @Point => m_Wrapper.m_Supervisor_Point;
            public InputAction @Click => m_Wrapper.m_Supervisor_Click;
            public InputAction @ScrollWheel => m_Wrapper.m_Supervisor_ScrollWheel;
            public InputAction @MiddleClick => m_Wrapper.m_Supervisor_MiddleClick;
            public InputAction @RightClick => m_Wrapper.m_Supervisor_RightClick;
            public InputAction @TrackedDevicePosition => m_Wrapper.m_Supervisor_TrackedDevicePosition;
            public InputAction @TrackedDeviceOrientation => m_Wrapper.m_Supervisor_TrackedDeviceOrientation;
            public InputActionMap Get() { return m_Wrapper.m_Supervisor; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SupervisorActions set) { return set.Get(); }
            public void SetCallbacks(ISupervisorActions instance)
            {
                if (m_Wrapper.m_SupervisorActionsCallbackInterface != null)
                {
                    @Navigate.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnNavigate;
                    @Navigate.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnNavigate;
                    @Navigate.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnNavigate;
                    @Submit.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnSubmit;
                    @Submit.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnSubmit;
                    @Submit.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnSubmit;
                    @Cancel.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnCancel;
                    @Point.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnPoint;
                    @Point.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnPoint;
                    @Point.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnPoint;
                    @Click.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnClick;
                    @ScrollWheel.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnScrollWheel;
                    @ScrollWheel.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnScrollWheel;
                    @ScrollWheel.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnScrollWheel;
                    @MiddleClick.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnMiddleClick;
                    @MiddleClick.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnMiddleClick;
                    @MiddleClick.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnMiddleClick;
                    @RightClick.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnRightClick;
                    @TrackedDevicePosition.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnTrackedDevicePosition;
                    @TrackedDevicePosition.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnTrackedDevicePosition;
                    @TrackedDevicePosition.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnTrackedDevicePosition;
                    @TrackedDeviceOrientation.started -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnTrackedDeviceOrientation;
                    @TrackedDeviceOrientation.performed -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnTrackedDeviceOrientation;
                    @TrackedDeviceOrientation.canceled -= m_Wrapper.m_SupervisorActionsCallbackInterface.OnTrackedDeviceOrientation;
                }
                m_Wrapper.m_SupervisorActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Navigate.started += instance.OnNavigate;
                    @Navigate.performed += instance.OnNavigate;
                    @Navigate.canceled += instance.OnNavigate;
                    @Submit.started += instance.OnSubmit;
                    @Submit.performed += instance.OnSubmit;
                    @Submit.canceled += instance.OnSubmit;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                    @Point.started += instance.OnPoint;
                    @Point.performed += instance.OnPoint;
                    @Point.canceled += instance.OnPoint;
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @ScrollWheel.started += instance.OnScrollWheel;
                    @ScrollWheel.performed += instance.OnScrollWheel;
                    @ScrollWheel.canceled += instance.OnScrollWheel;
                    @MiddleClick.started += instance.OnMiddleClick;
                    @MiddleClick.performed += instance.OnMiddleClick;
                    @MiddleClick.canceled += instance.OnMiddleClick;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                    @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                    @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                    @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                    @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                    @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
                }
            }
        }
        public SupervisorActions @Supervisor => new SupervisorActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        private int m_CockpitSchemeIndex = -1;
        public InputControlScheme CockpitScheme
        {
            get
            {
                if (m_CockpitSchemeIndex == -1) m_CockpitSchemeIndex = asset.FindControlSchemeIndex("Cockpit");
                return asset.controlSchemes[m_CockpitSchemeIndex];
            }
        }
        public interface ISubjectActions
        {
            void OnValueChanged(InputAction.CallbackContext context);
            void OnNeutralCommandTriggered(InputAction.CallbackContext context);
            void OnPositiveCommandTriggered(InputAction.CallbackContext context);
            void OnNegativeCommandTriggered(InputAction.CallbackContext context);
            void OnUserResponse(InputAction.CallbackContext context);
        }
        public interface ISupervisorActions
        {
            void OnNavigate(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
            void OnClick(InputAction.CallbackContext context);
            void OnScrollWheel(InputAction.CallbackContext context);
            void OnMiddleClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnTrackedDevicePosition(InputAction.CallbackContext context);
            void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
        }
    }
}
