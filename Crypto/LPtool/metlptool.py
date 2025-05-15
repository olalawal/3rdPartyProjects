import streamlit as st
import requests
import pandas as pd
import json
from io import StringIO
import time

# Set page configuration
st.set_page_config(
    page_title="Meteora LP Position Tracker",
    page_icon="📊",
    layout="wide",
)

# Custom CSS for better styling
st.markdown("""
<style>
    .main {
        padding: 2rem;
    }
    .stTabs [data-baseweb="tab-list"] {
        gap: 8px;
    }
    .stTabs [data-baseweb="tab"] {
        padding: 10px 16px;
        border-radius: 4px 4px 0px 0px;
    }
    .header-container {
        display: flex;
        align-items: center;
        gap: 20px;
    }
    .metric-card {
        background-color: #f0f2f6;
        border-radius: 10px;
        padding: 15px;
        margin: 10px 0;
        box-shadow: 0 2px 5px rgba(0,0,0,0.05);
    }
    .token-pair {
        font-weight: bold;
        font-size: 1.1rem;
    }
</style>
""", unsafe_allow_html=True)

# Header
st.markdown("<div class='header-container'>"
            "<h1>📊 Meteora LP Position Tracker</h1>"
            "</div>", unsafe_allow_html=True)

st.markdown("""
This app allows you to track LP positions on the Meteora protocol for multiple wallet addresses.
Upload a text file with up to 5 Solana wallet addresses (one per line) to get started.
""")

# Function to fetch data from the API
def fetch_lp_positions(wallet_address):
    url = f"https://api.lpagent.io/api/v1/lp-bot/lp-positions/overview/{wallet_address}?protocol=meteora"
    try:
        response = requests.get(url)
        if response.status_code == 200:
            return response.json()
        else:
            st.error(f"Error fetching data for {wallet_address}: {response.status_code}")
            return None
    except Exception as e:
        st.error(f"Error fetching data for {wallet_address}: {str(e)}")
        return None

# Function to parse and display LP position data
def display_lp_position(position_data, wallet_address):
    if not position_data:
        st.warning(f"No data available for wallet: {wallet_address}")
        return
    
    try:
        # Display summary stats
        col1, col2, col3 = st.columns(3)
        
        # Check if totalValue exists in the data
        total_value = position_data.get('totalValue', 0)
        with col1:
            st.metric("Total Value", f"${total_value:,.2f}")
        
        # Check if other metrics are available
        with col2:
            fees_earned = position_data.get('feesEarned', 0)
            st.metric("Fees Earned", f"${fees_earned:,.2f}")
        
        with col3:
            position_count = len(position_data.get('positions', []))
            st.metric("Number of Positions", position_count)
        
        # Display positions in a table
        if 'positions' in position_data and position_data['positions']:
            positions = position_data['positions']
            
            # Create a DataFrame for display
            position_data_list = []
            
            for pos in positions:
                position_info = {
                    "Pool": f"{pos.get('token0Symbol', 'Unknown')}/{pos.get('token1Symbol', 'Unknown')}",
                    "Token 0": pos.get('token0Symbol', 'Unknown'),
                    "Amount 0": pos.get('token0Amount', 0),
                    "Token 1": pos.get('token1Symbol', 'Unknown'),
                    "Amount 1": pos.get('token1Amount', 0),
                    "Current Value": f"${pos.get('currentValue', 0):,.2f}",
                    "Fees Earned": f"${pos.get('feesEarned', 0):,.2f}",
                    "APR": f"{pos.get('apr', 0):.2f}%"
                }
                position_data_list.append(position_info)
            
            df = pd.DataFrame(position_data_list)
            st.dataframe(df, use_container_width=True)
            
            # Detailed position information
            if position_data_list:
                st.subheader("Position Details")
                for i, pos in enumerate(positions):
                    with st.expander(f"{pos.get('token0Symbol', 'Unknown')}/{pos.get('token1Symbol', 'Unknown')} Position"):
                        st.json(pos)
        else:
            st.info("No LP positions found for this wallet on Meteora.")
    
    except Exception as e:
        st.error(f"Error processing data: {str(e)}")
        st.json(position_data)  # Display raw data for debugging

# File uploader for wallet addresses
uploaded_file = st.file_uploader("Upload a text file with wallet addresses (one per line, max 5)", type=['txt'])

if uploaded_file:
    # Read wallet addresses from the file
    content = uploaded_file.getvalue().decode("utf-8")
    wallet_addresses = [line.strip() for line in content.split('\n') if line.strip()]
    
    # Limit to 5 wallets
    if len(wallet_addresses) > 5:
        st.warning("Only the first 5 wallet addresses will be processed.")
        wallet_addresses = wallet_addresses[:5]
    
    # Create tabs for each wallet
    if wallet_addresses:
        tabs = st.tabs([f"Wallet {i+1}: {addr[:6]}...{addr[-4:]}" for i, addr in enumerate(wallet_addresses)])
        
        # Process each wallet in its respective tab
        for i, (tab, addr) in enumerate(zip(tabs, wallet_addresses)):
            with tab:
                st.write(f"**Full Address:** {addr}")
                
                # Add a button to fetch data (to avoid automatic API calls)
                fetch_button = st.button(f"Fetch LP Positions for Wallet {i+1}", key=f"fetch_{i}")
                
                if fetch_button:
                    with st.spinner(f"Fetching data for wallet {i+1}..."):
                        position_data = fetch_lp_positions(addr)
                        if position_data:
                            st.success("Data retrieved successfully!")
                            display_lp_position(position_data, addr)
                            
                            # Option to download the JSON data
                            json_str = json.dumps(position_data, indent=2)
                            st.download_button(
                                label="Download JSON Data",
                                data=json_str,
                                file_name=f"meteora_lp_{addr[:6]}_{int(time.time())}.json",
                                mime="application/json"
                            )
else:
    # Sample data display for demonstration
    st.info("No file uploaded. Please upload a text file with wallet addresses to get started.")
    
    # Display a sample wallet format
    st.markdown("""
    ### Sample Format
    Your text file should contain wallet addresses, one per line:
    ```
    66Q7nHaRDzbBib9smvFZndCXe6TxaX6QKStd692MuG7V
    AnotherWalletAddressHere...
    YetAnotherWalletAddressHere...
    ```
    """)

# Footer
st.markdown("---")
st.markdown("""
<div style="text-align: center; color: #888;">
    <p>Created with Streamlit • Powered by Meteora LP Agent API</p>
</div>
""", unsafe_allow_html=True)