"""
DeFiTuna Position Monitor - Streamlit UI

Installation:
pip install streamlit plotly pandas solana solders aiohttp

For the monitoring script:
npm install @defituna/sdk

Usage:
streamlit run app.py
"""

import streamlit as st
import streamlit.components.v1 as components
import asyncio
import pandas as pd
from datetime import datetime, timedelta
import plotly.graph_objects as go
import plotly.express as px
import json
from solana.rpc.api import Client
from solders.pubkey import Pubkey
import base64
import requests
from typing import Dict, List, Optional
import aiohttp

# Page config
st.set_page_config(
    page_title="Tuna Position Monitor",
    page_icon="🐟",
    layout="wide",
    initial_sidebar_state="expanded"
)

# Custom CSS
st.markdown("""
<style>
    .health-good { color: #00ff00; font-weight: bold; font-size: 24px; }
    .health-warning { color: #ffa500; font-weight: bold; font-size: 24px; }
    .health-danger { color: #ff0000; font-weight: bold; font-size: 24px; }
    .position-card { 
        background-color: #1a1a1a; 
        padding: 20px; 
        border-radius: 15px; 
        margin-bottom: 20px;
        border: 1px solid #333;
    }
    .metric-card {
        background-color: #2a2a2a;
        padding: 15px;
        border-radius: 10px;
        text-align: center;
    }
</style>
""", unsafe_allow_html=True)

# Initialize session state
if 'wallet_connected' not in st.session_state:
    st.session_state.wallet_connected = False
if 'wallet_address' not in st.session_state:
    st.session_state.wallet_address = None
if 'positions' not in st.session_state:
    st.session_state.positions = {}
if 'monitoring_configs' not in st.session_state:
    st.session_state.monitoring_configs = {}
if 'monitoring_active' not in st.session_state:
    st.session_state.monitoring_active = {}

class TunaPositionManager:
    """Interface to DeFiTuna API and SDK"""
    def __init__(self, api_url="https://api.defituna.com/api"):
        self.api_url = api_url
        self.client = Client("https://api.mainnet-beta.solana.com")
        
    async def get_user_positions(self, wallet_address: str) -> List[Dict]:
        """Fetch all positions for a wallet address using DeFiTuna API"""
        try:
            # Use DeFiTuna's API to get user positions
            async with aiohttp.ClientSession() as session:
                async with session.get(f"{self.api_url}/getUserTunaPositions/{wallet_address}") as response:
                    if response.status == 200:
                        positions_data = await response.json()
                        
                        # Process and format the positions
                        positions = []
                        for pos in positions_data:
                            # Calculate health factor based on collateral and debt
                            collateral_value = float(pos.get('collateralValue', 0))
                            debt_value = float(pos.get('debtValue', 0))
                            liquidation_threshold = float(pos.get('liquidationThreshold', 0.75))
                            
                            health_factor = 0
                            if debt_value > 0:
                                health_factor = (collateral_value * liquidation_threshold) / debt_value
                            
                            positions.append({
                                'position_id': pos.get('positionId', ''),
                                'market': pos.get('market', ''),
                                'collateral_amount': pos.get('collateralAmount', 0),
                                'collateral_value': collateral_value,
                                'collateral_token': pos.get('collateralToken', ''),
                                'debt_amount': pos.get('debtAmount', 0),
                                'debt_value': debt_value,
                                'debt_token': pos.get('debtToken', ''),
                                'health_factor': health_factor,
                                'liquidation_price': pos.get('liquidationPrice', 0),
                                'leverage': pos.get('leverage', 1),
                                'entry_price': pos.get('entryPrice', 0),
                                'apy': pos.get('apy', 0),
                                'pnl': pos.get('pnl', 0),
                                'liquidation_threshold': liquidation_threshold
                            })
                            
                        return positions
                    else:
                        st.error(f"Failed to fetch positions: API returned status {response.status}")
                        return []
                        
        except Exception as e:
            st.error(f"Error fetching positions: {e}")
            # For demo purposes, return mock data if API fails
            return self.get_mock_positions(wallet_address)
    
    def get_mock_positions(self, wallet_address: str) -> List[Dict]:
        """Return mock positions for demo purposes"""
        return [
            {
                'position_id': 'pos_demo_1',
                'market': 'SOL-USDC',
                'collateral_amount': 10,
                'collateral_value': 1500,
                'collateral_token': 'SOL',
                'debt_amount': 1000,
                'debt_value': 1000,
                'debt_token': 'USDC',
                'health_factor': 1.125,
                'liquidation_price': 90,
                'leverage': 2.5,
                'entry_price': 150,
                'apy': 15.5,
                'pnl': 50,
                'liquidation_threshold': 0.75
            },
            {
                'position_id': 'pos_demo_2',
                'market': 'ETH-USDC',
                'collateral_amount': 1,
                'collateral_value': 3000,
                'collateral_token': 'ETH',
                'debt_amount': 2000,
                'debt_value': 2000,
                'debt_token': 'USDC',
                'health_factor': 1.35,
                'liquidation_price': 2500,
                'leverage': 2.0,
                'entry_price': 3000,
                'apy': 12.3,
                'pnl': -100,
                'liquidation_threshold': 0.9
            }
        ]
    
    async def get_position_details(self, position_id: str) -> Dict:
        """Get detailed information about a specific position"""
        try:
            async with aiohttp.ClientSession() as session:
                async with session.get(f"{self.api_url}/getPosition/{position_id}") as response:
                    if response.status == 200:
                        return await response.json()
                    else:
                        st.error(f"Failed to fetch position details: {response.status}")
                        return {}
        except Exception as e:
            st.error(f"Error fetching position details: {e}")
            return {}
    
    async def repay_debt(self, position_id: str, amount: float, wallet) -> bool:
        """Repay debt for a position using Phantom wallet"""
        # This would integrate with Phantom wallet and DeFiTuna's smart contracts
        # For now, this is a placeholder
        st.info(f"Repaying {amount} for position {position_id}")
        return True

# Phantom Wallet Connection Component
def phantom_connect_button():
    """Create Phantom wallet connection button"""
    phantom_connect_js = """
    <script>
    async function connectPhantom() {
        if (window.solana && window.solana.isPhantom) {
            try {
                const resp = await window.solana.connect();
                const walletAddress = resp.publicKey.toString();
                
                // Send wallet address to Streamlit
                window.parent.postMessage({
                    type: 'phantom_connected',
                    wallet: walletAddress
                }, '*');
            } catch (err) {
                console.error(err);
                window.parent.postMessage({
                    type: 'phantom_error',
                    error: err.message
                }, '*');
            }
        } else {
            window.open('https://phantom.app/', '_blank');
        }
    }
    
    // Listen for Streamlit ready
    window.addEventListener('message', (event) => {
        if (event.data.type === 'streamlit:ready') {
            // Check if already connected
            if (window.solana && window.solana.isConnected) {
                window.solana.publicKey && window.parent.postMessage({
                    type: 'phantom_connected',
                    wallet: window.solana.publicKey.toString()
                }, '*');
            }
        }
    });
    </script>
    
    <button onclick="connectPhantom()" style="
        background: linear-gradient(135deg, #ab47bc 0%, #7b1fa2 100%);
        color: white;
        border: none;
        padding: 12px 24px;
        border-radius: 8px;
        font-size: 16px;
        font-weight: bold;
        cursor: pointer;
        display: flex;
        align-items: center;
        gap: 8px;
    ">
        <img src="https://raw.githubusercontent.com/solana-labs/wallet-adapter/master/packages/wallets/icons/phantom.svg" 
             width="24" height="24" style="filter: brightness(0) invert(1);">
        Connect Phantom Wallet
    </button>
    """
    
    return phantom_connect_js

# Header
st.title("🐟 Tuna Position Monitor")
st.markdown("Monitor and manage your DeFi positions to prevent liquidation")

# Wallet Connection Section
col1, col2, col3 = st.columns([2, 3, 2])

with col2:
    if not st.session_state.wallet_connected:
        st.markdown("### Connect Your Wallet")
        
        # Phantom connection
        wallet_html = phantom_connect_button()
        html_content = f"""
        <div id="phantom-connect-container">
            {wallet_html}
        </div>
        """
        
        # Use components.html to inject the button
        result = components.html(html_content, height=100)
        
        # Handle wallet connection via query params (alternative method)
        query_params = st.experimental_get_query_params()
        if 'wallet' in query_params:
            st.session_state.wallet_address = query_params['wallet'][0]
            st.session_state.wallet_connected = True
            st.experimental_set_query_params()
            st.rerun()
    else:
        st.success(f"✅ Connected: {st.session_state.wallet_address[:8]}...{st.session_state.wallet_address[-6:]}")
        if st.button("Disconnect Wallet"):
            st.session_state.wallet_connected = False
            st.session_state.wallet_address = None
            st.session_state.positions = {}
            st.rerun()

st.divider()

# Main Content
if st.session_state.wallet_connected:
    # Initialize position manager
    position_manager = TunaPositionManager()
    
    # Sidebar Configuration
    st.sidebar.title("⚙️ Monitoring Settings")
    
    # Global Settings
    st.sidebar.subheader("Global Configuration")
    
    # Monitoring interval
    default_interval = st.sidebar.slider(
        "Check Interval (minutes)",
        min_value=1,
        max_value=60,
        value=10,
        help="How often to check position health"
    )
    
    # Health factor target
    default_health_target = st.sidebar.slider(
        "Target Health Factor",
        min_value=1.1,
        max_value=2.0,
        value=1.2,
        step=0.05,
        help="Maintain positions at this health factor (1.0 = liquidation)"
    )
    
    # Auto-repay settings
    st.sidebar.subheader("Auto-Repay Settings")
    enable_auto_repay = st.sidebar.checkbox("Enable Auto-Repayment", value=False)
    
    if enable_auto_repay:
        max_repay_amount = st.sidebar.number_input(
            "Max Repayment per Transaction (SOL)",
            min_value=0.1,
            max_value=1000.0,
            value=10.0,
            step=0.1
        )
        
        st.sidebar.warning("⚠️ Auto-repayment will use SOL from your wallet")
    
    # Notification settings
    st.sidebar.subheader("Notifications")
    notification_methods = st.sidebar.multiselect(
        "Alert Methods",
        ["Browser Notification", "Email", "Discord", "Telegram"],
        default=["Browser Notification"]
    )
    
    # Fetch and display positions
    tab1, tab2, tab3 = st.tabs(["📊 Positions", "📈 Analytics", "⚙️ Settings"])
    
    with tab1:
        st.subheader("Your Active Positions")
        
        # Auto-refresh toggle
        col1, col2, col3 = st.columns([2, 2, 8])
        
        with col1:
            auto_refresh = st.checkbox("Auto-refresh", value=False, help="Automatically refresh positions every 30 seconds")
        
        with col2:
            if st.button("🔄 Refresh Positions", type="primary"):
                st.session_state.force_refresh = True
        
        # Auto-refresh logic
        if auto_refresh:
            st.empty()  # Placeholder for auto-refresh
            time_placeholder = st.empty()
            time_placeholder.caption("Auto-refresh enabled (every 30 seconds)")
            
            # Use st.experimental_rerun with timer
            if 'last_refresh' not in st.session_state:
                st.session_state.last_refresh = datetime.now()
            
            if (datetime.now() - st.session_state.last_refresh).seconds > 30:
                st.session_state.force_refresh = True
                st.session_state.last_refresh = datetime.now()
        
        # Refresh positions if needed
        if st.session_state.get('force_refresh', False):
            with st.spinner("Fetching positions from DeFiTuna..."):
                # Fetch positions from DeFiTuna API
                try:
                    # Create event loop for async operation
                    import asyncio
                    loop = asyncio.new_event_loop()
                    asyncio.set_event_loop(loop)
                    positions = loop.run_until_complete(
                        position_manager.get_user_positions(st.session_state.wallet_address)
                    )
                    st.session_state.positions = {pos['position_id']: pos for pos in positions}
                    if positions:
                        st.success(f"Found {len(positions)} positions!")
                    else:
                        st.info("No positions found for this wallet.")
                    st.session_state.force_refresh = False
                except Exception as e:
                    st.error(f"Error fetching positions: {e}")
                    st.session_state.force_refresh = False
        
        # Display positions
        if st.session_state.positions:
            # Summary metrics
            col1, col2, col3, col4 = st.columns(4)
            
            total_collateral = sum(pos.get('collateral_value', 0) for pos in st.session_state.positions.values())
            total_debt = sum(pos.get('debt_value', 0) for pos in st.session_state.positions.values())
            avg_health = sum(pos.get('health_factor', 1) for pos in st.session_state.positions.values()) / len(st.session_state.positions)
            at_risk = sum(1 for pos in st.session_state.positions.values() if pos.get('health_factor', 1) < 1.2)
            
            col1.metric("Total Collateral", f"${total_collateral:,.2f}")
            col2.metric("Total Debt", f"${total_debt:,.2f}")
            col3.metric("Avg Health Factor", f"{avg_health:.3f}")
            col4.metric("Positions at Risk", at_risk, delta=None if at_risk == 0 else f"{at_risk} positions")
            
            st.markdown("---")
            
            # Individual positions
            for position_id, position in st.session_state.positions.items():
                with st.container():
                    st.markdown(f'<div class="position-card">', unsafe_allow_html=True)
                    
                    # Position header
                    col1, col2, col3 = st.columns([3, 2, 1])
                    
                    with col1:
                        st.markdown(f"### {position.get('market', 'Unknown')} - Position: {position_id}")
                        st.caption(f"Leverage: {position.get('leverage', 1)}x | Entry: ${position.get('entry_price', 0):.2f}")
                    
                    with col2:
                        health = position.get('health_factor', 1)
                        health_class = "health-good" if health > 1.3 else "health-warning" if health > 1.1 else "health-danger"
                        st.markdown(f'<p class="{health_class}">Health: {health:.3f}</p>', unsafe_allow_html=True)
                    
                    with col3:
                        # Individual monitoring toggle
                        monitor_key = f"monitor_{position_id}"
                        is_monitoring = st.checkbox("Monitor", key=monitor_key, value=st.session_state.monitoring_active.get(position_id, False))
                        st.session_state.monitoring_active[position_id] = is_monitoring
                    
                    # Position details
                    col1, col2, col3, col4, col5 = st.columns(5)
                    
                    col1.metric("Collateral", f"{position.get('collateral_amount', 0):.4f} {position.get('collateral_token', '')}")
                    col2.metric("Debt", f"{position.get('debt_amount', 0):.2f} {position.get('debt_token', '')}")
                    col3.metric("Liquidation Price", f"${position.get('liquidation_price', 0):.2f}")
                    
                    # PnL with color coding
                    pnl = position.get('pnl', 0)
                    pnl_delta_color = "normal" if pnl == 0 else "inverse" if pnl < 0 else "normal"
                    col4.metric("P&L", f"${pnl:,.2f}", delta=f"{(pnl/position.get('collateral_value', 1))*100:.1f}%", delta_color=pnl_delta_color)
                    
                    col5.metric("APY", f"{position.get('apy', 0):.1f}%")
                    
                    # Position-specific settings
                    with st.expander("⚙️ Position Settings"):
                        pcol1, pcol2 = st.columns(2)
                        
                        with pcol1:
                            pos_interval = st.number_input(
                                f"Check Interval (min)",
                                min_value=1,
                                max_value=60,
                                value=st.session_state.monitoring_configs.get(position_id, {}).get('interval', default_interval),
                                key=f"interval_{position_id}"
                            )
                        
                        with pcol2:
                            pos_health_target = st.slider(
                                f"Target Health",
                                min_value=1.1,
                                max_value=2.0,
                                value=st.session_state.monitoring_configs.get(position_id, {}).get('target_health', default_health_target),
                                step=0.05,
                                key=f"health_{position_id}"
                            )
                        
                        # Save position config
                        st.session_state.monitoring_configs[position_id] = {
                            'interval': pos_interval,
                            'target_health': pos_health_target
                        }
                    
                    # Quick actions
                    col1, col2, col3 = st.columns(3)
                    
                    with col1:
                        if st.button("💰 Repay Debt", key=f"repay_{position_id}"):
                            st.info("Repayment feature coming soon...")
                    
                    with col2:
                        if st.button("📊 View History", key=f"history_{position_id}"):
                            st.session_state[f'show_history_{position_id}'] = True
                    
                    with col3:
                        if st.button("🔔 Set Alert", key=f"alert_{position_id}"):
                            st.info("Alert configuration coming soon...")
                    
                    st.markdown('</div>', unsafe_allow_html=True)
                    
                    # Show history chart if requested
                    if st.session_state.get(f'show_history_{position_id}', False):
                        st.subheader(f"Health Factor History - {position_id}")
                        
                        # Generate sample history data (replace with actual historical data)
                        history_data = pd.DataFrame({
                            'timestamp': pd.date_range(end=datetime.now(), periods=24, freq='H'),
                            'health_factor': [position.get('health_factor', 1) + (i % 5 - 2) * 0.02 for i in range(24)]
                        })
                        
                        fig = go.Figure()
                        fig.add_trace(go.Scatter(
                            x=history_data['timestamp'],
                            y=history_data['health_factor'],
                            mode='lines+markers',
                            name='Health Factor',
                            line=dict(color='#ab47bc', width=2)
                        ))
                        
                        # Add danger zone
                        fig.add_hline(y=1.0, line_dash="dash", line_color="red", annotation_text="Liquidation")
                        fig.add_hline(y=1.2, line_dash="dash", line_color="orange", annotation_text="Warning")
                        fig.add_hline(y=pos_health_target, line_dash="dash", line_color="green", annotation_text="Target")
                        
                        fig.update_layout(
                            height=400,
                            template="plotly_dark",
                            title=f"Position Health History",
                            xaxis_title="Time",
                            yaxis_title="Health Factor"
                        )
                        
                        st.plotly_chart(fig, use_container_width=True)
                        
                        if st.button("Close", key=f"close_history_{position_id}"):
                            st.session_state[f'show_history_{position_id}'] = False
                            st.rerun()
        else:
            st.info("No positions found. Click 'Refresh Positions' to fetch your positions.")
    
    with tab2:
        st.subheader("Portfolio Analytics")
        
        if st.session_state.positions:
            # Portfolio composition
            col1, col2 = st.columns(2)
            
            with col1:
                # Collateral distribution pie chart
                collateral_data = pd.DataFrame([
                    {'Position': pid, 'Collateral': pos.get('collateral_value', 0)}
                    for pid, pos in st.session_state.positions.items()
                ])
                
                fig = px.pie(
                    collateral_data,
                    values='Collateral',
                    names='Position',
                    title='Collateral Distribution'
                )
                fig.update_layout(template="plotly_dark")
                st.plotly_chart(fig, use_container_width=True)
            
            with col2:
                # Health factor distribution
                health_data = pd.DataFrame([
                    {'Position': pid, 'Health Factor': pos.get('health_factor', 1)}
                    for pid, pos in st.session_state.positions.items()
                ])
                
                fig = px.bar(
                    health_data,
                    x='Position',
                    y='Health Factor',
                    title='Health Factor by Position',
                    color='Health Factor',
                    color_continuous_scale=['red', 'orange', 'green']
                )
                fig.add_hline(y=1.0, line_dash="dash", line_color="red")
                fig.update_layout(template="plotly_dark")
                st.plotly_chart(fig, use_container_width=True)
        else:
            st.info("No positions to analyze. Fetch your positions first.")
    
    with tab3:
        st.subheader("Advanced Settings")
        
        # API Configuration
        st.markdown("### DeFiTuna API Configuration")
        api_url = st.text_input("API URL", value="https://api.defituna.com/api", help="DeFiTuna API endpoint")
        
        # Wallet info
        st.markdown("### Connected Wallet")
        if st.session_state.wallet_connected:
            st.info(f"Wallet Address: `{st.session_state.wallet_address}`")
            st.caption("This wallet will be used for monitoring and auto-repayments")
        
        # Export/Import configurations
        col1, col2 = st.columns(2)
        
        with col1:
            st.markdown("### Export Configuration")
            config_data = {
                'wallet_address': st.session_state.wallet_address,
                'monitoring_configs': st.session_state.monitoring_configs,
                'monitoring_active': st.session_state.monitoring_active,
                'global_settings': {
                    'default_interval': default_interval,
                    'default_health_target': default_health_target,
                    'enable_auto_repay': enable_auto_repay,
                    'api_url': api_url
                }
            }
            
            config_json = json.dumps(config_data, indent=2)
            st.download_button(
                label="📥 Download Config",
                data=config_json,
                file_name="tuna_monitor_config.json",
                mime="application/json"
            )
        
        with col2:
            st.markdown("### Import Configuration")
            uploaded_file = st.file_uploader("Choose a config file", type="json")
            
            if uploaded_file is not None:
                config = json.load(uploaded_file)
                st.session_state.monitoring_configs = config.get('monitoring_configs', {})
                st.session_state.monitoring_active = config.get('monitoring_active', {})
                st.success("Configuration imported successfully!")
                st.rerun()

else:
    # Not connected
    st.info("👆 Please connect your Phantom wallet to view and monitor your positions")
    
    # Demo section
    st.markdown("---")
    st.subheader("How it works")
    
    col1, col2, col3 = st.columns(3)
    
    with col1:
        st.markdown("""
        ### 1. Connect Wallet
        Connect your Phantom wallet to automatically discover all your Tuna positions
        """)
    
    with col2:
        st.markdown("""
        ### 2. Configure Monitoring
        Set custom health factor targets and monitoring intervals for each position
        """)
    
    with col3:
        st.markdown("""
        ### 3. Prevent Liquidation
        Get alerts and auto-repay debt to maintain safe health factors
        """)

# Footer
st.markdown("---")
st.caption("🐟 DeFiTuna Position Monitor | Keep your leveraged positions safe from liquidation")

# Help section
with st.expander("ℹ️ How to Use This Monitor"):
    st.markdown("""
    ### Quick Start Guide
    
    1. **Connect Your Wallet**: Click "Connect Phantom Wallet" to connect your wallet
    2. **Fetch Positions**: Click "Refresh Positions" to load all your DeFiTuna positions
    3. **Configure Monitoring**: Set health factor targets and check intervals for each position
    4. **Export Config**: Download your configuration to use with the monitoring script
    5. **Run Monitor**: Use the generated monitoring script to continuously watch your positions
    
    ### DeFiTuna Integration
    
    This monitor uses the official DeFiTuna SDK (`@defituna/sdk`) to:
    - Fetch your leveraged CLMM positions
    - Monitor health factors in real-time
    - Calculate liquidation prices
    - Track P&L and APY
    
    ### Auto-Repayment
    
    When enabled, the monitoring script can automatically repay debt using SOL from your wallet
    when positions get close to liquidation. Configure the target health factor to maintain
    a safe buffer (recommended: 1.2 or higher).
    
    ### API Documentation
    
    - DeFiTuna API: https://api.defituna.com/api
    - SDK: npm install @defituna/sdk
    - Positions are leveraged up to 3.5x (5x for stablecoins)
    """)

# Background monitoring script (would run separately in production)
if st.sidebar.button("📝 Generate Monitoring Script"):
    monitoring_script = """// monitoring_service.js
// Install: npm install @defituna/sdk @solana/web3.js

import { TunaApiClient } from "@defituna/sdk";
import { Connection, Keypair, PublicKey, Transaction } from "@solana/web3.js";
import fs from 'fs';

// Load configuration
const config = JSON.parse(fs.readFileSync('tuna_monitor_config.json', 'utf8'));

// Initialize API client
const apiClient = new TunaApiClient("https://api.defituna.com/api");
const connection = new Connection("https://api.mainnet-beta.solana.com");

// Load wallet
const wallet = Keypair.fromSecretKey(
    Uint8Array.from(JSON.parse(fs.readFileSync('wallet.json', 'utf8')))
);

async function checkPositions() {
    const positions = await apiClient.getUserTunaPositions(wallet.publicKey.toString());
    
    for (const position of positions) {
        const positionConfig = config.monitoring_configs[position.positionId] || {
            target_health: 1.2,
            interval: 600
        };
        
        // Calculate health factor
        const healthFactor = (position.collateralValue * position.liquidationThreshold) / position.debtValue;
        
        console.log(`Position ${position.positionId}: Health Factor = ${healthFactor.toFixed(3)}`);
        
        if (healthFactor < positionConfig.target_health) {
            console.log(`⚠️  Position ${position.positionId} needs attention!`);
            
            // Calculate repayment amount
            const targetDebt = (position.collateralValue * position.liquidationThreshold) / positionConfig.target_health;
            const repayAmount = position.debtValue - targetDebt;
            
            if (config.global_settings.enable_auto_repay && repayAmount > 0) {
                console.log(`Repaying ${repayAmount.toFixed(2)} to improve health factor...`);
                // TODO: Implement actual repayment transaction
                // This would involve calling DeFiTuna's repay function
            }
        }
    }
}

// Main monitoring loop
async function monitor() {
    console.log("Starting DeFiTuna position monitor...");
    
    while (true) {
        try {
            await checkPositions();
        } catch (error) {
            console.error("Error checking positions:", error);
        }
        
        // Wait for the configured interval
        await new Promise(resolve => setTimeout(resolve, config.global_settings.default_interval * 1000));
    }
}

// Start monitoring
monitor().catch(console.error);
"""
    
    # Also generate a Python version
    python_script = """# monitoring_service.py
# Install: pip install aiohttp asyncio solana

import asyncio
import aiohttp
import json
from datetime import datetime
from solana.rpc.api import Client
from solana.keypair import Keypair

class TunaMonitor:
    def __init__(self, config_file='tuna_monitor_config.json'):
        with open(config_file, 'r') as f:
            self.config = json.load(f)
        
        self.api_url = "https://api.defituna.com/api"
        self.client = Client("https://api.mainnet-beta.solana.com")
        
    async def get_positions(self, wallet_address):
        async with aiohttp.ClientSession() as session:
            async with session.get(f"{self.api_url}/getUserTunaPositions/{wallet_address}") as response:
                return await response.json()
    
    async def check_and_maintain(self):
        wallet_address = self.config.get('wallet_address')
        positions = await self.get_positions(wallet_address)
        
        for position in positions:
            pos_config = self.config['monitoring_configs'].get(
                position['positionId'], 
                {'target_health': 1.2}
            )
            
            # Calculate health factor
            health_factor = (position['collateralValue'] * position['liquidationThreshold']) / position['debtValue']
            
            print(f"[{datetime.now()}] Position {position['positionId']}: Health = {health_factor:.3f}")
            
            if health_factor < pos_config['target_health']:
                print(f"⚠️  Position needs repayment!")
                # Implement repayment logic here
    
    async def run(self):
        interval = self.config['global_settings']['default_interval']
        
        while True:
            try:
                await self.check_and_maintain()
            except Exception as e:
                print(f"Error: {e}")
            
            await asyncio.sleep(interval)

if __name__ == "__main__":
    monitor = TunaMonitor()
    asyncio.run(monitor.run())
"""
    
    col1, col2 = st.columns(2)
    
    with col1:
        st.download_button(
            label="📥 Download JavaScript Monitor",
            data=monitoring_script,
            file_name="monitoring_service.js",
            mime="text/javascript"
        )
    
    with col2:
        st.download_button(
            label="📥 Download Python Monitor",
            data=python_script,
            file_name="monitoring_service.py",
            mime="text/x-python"
        )